using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using PocParalell.Domain;
using PocParalell.Extensions;
using PocParalell.Infrastructure.Interfaces;
using PocParalell.Services.Interfaces;
using System.Diagnostics;

namespace PocParalell.Services
{
    public class CalculoService : ICalculoService
    {
        private readonly ICdiRepository _repository;
        private readonly IPosicaoRepository _posicaoRepository;
        private readonly ICalculoRepository _calculoRepository;
        private readonly IExecucaoRepository _execucaoRepository;
        public CalculoService(ICdiRepository repository, IPosicaoRepository posicaoRepository, ICalculoRepository calculoRepository, IExecucaoRepository execucaoRepository)
        {
            _repository = repository;
            _posicaoRepository = posicaoRepository;
            _calculoRepository = calculoRepository;
            _execucaoRepository = execucaoRepository;
        }
        public async Task InsereUsandoForeach()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var posicoes = await _posicaoRepository.ObterTodosAsync();
            var currentCdi = await _repository.BuscarAsync(x => x.Data.Date == DateTime.Now.Date.AddDays(-5));

            foreach (var posicao in posicoes)
            {
                var diasUteis = posicao.Data.BusinessDaysUntil(DateTime.Now);
                var initCdi = await _repository.BuscarAsync(x => x.Data.Date == posicao.Data);
                var calculo = CalculatorExtensions.CalculateAmount(posicao.Valor, posicao.Quantidade, initCdi.Valor, currentCdi.Valor, diasUteis);
                var resultado = new PocCalculo
                {
                    IdPosicao = posicao.Id,
                    TipoCalculo = 1,
                    Conta = posicao.Conta,
                    Valor = calculo
                };
                await _calculoRepository.AdicionarAsync(resultado);
            }

            stopwatch.Stop();
            var tempo = stopwatch.Elapsed;
            var duracao = tempo.ToString("mm':'ss':'fff");
            var execucao = new PocExecucao()
            {
                TipoCalculo = 1,
                Tempo = duracao,
                Data = DateTime.Now
            };
            await _execucaoRepository.AdicionarAsync(execucao);
        }

        public async Task InsereUsandoPaginacaoBulk()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int numberOfObjectsPerPage = 100;
            var initPage = 1;
            var totalRegister = _posicaoRepository.TotalPosicoes();
            var totalPages = 10;
            var currentCdi = await _repository.BuscarAsync(x => x.Data.Date == DateTime.Now.Date.AddDays(-5));
            for (var i = 0; i <= totalPages; i++) 
            {
                var posicoes = _posicaoRepository.ObterPosicoes(i, numberOfObjectsPerPage);
                var calculos = new List<PocCalculo>();
                foreach (var posicao in posicoes)
                {
                    var diasUteis = posicao.Data.BusinessDaysUntil(DateTime.Now);
                    var initCdi = await _repository.BuscarAsync(x => x.Data.Date == posicao.Data);
                    var calculo = CalculatorExtensions.CalculateAmount(posicao.Valor, posicao.Quantidade, initCdi.Valor, currentCdi.Valor, diasUteis);
                    var resultado = new PocCalculo
                    {
                        IdPosicao = posicao.Id,
                        TipoCalculo = 2,
                        Conta = posicao.Conta,
                        Valor = calculo
                    };
                    calculos.Add(resultado);
                }
                await _calculoRepository.AdicionarLoteAsync(calculos);
            }

            stopwatch.Stop();
            var tempo = stopwatch.Elapsed;
            var duracao = tempo.ToString("mm':'ss':'fff");
            var execucao = new PocExecucao()
            {
                TipoCalculo = 2,
                Tempo = duracao,
                Data = DateTime.Now
            };
            await _execucaoRepository.AdicionarAsync(execucao);
        }

        public async Task InsereUsandoTasks()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var posicoes = await _posicaoRepository.ObterTodosAsync();
            var currentCdi = await _repository.BuscarAsync(x => x.Data.Date == DateTime.Now.Date.AddDays(-5));

            var tasks = posicoes.Select(async posicao =>
            {
                var diasUteis = posicao.Data.BusinessDaysUntil(DateTime.Now);
                var initCdi = await _repository.BuscarAsync(x => x.Data.Date == posicao.Data);
                var calculo = CalculatorExtensions.CalculateAmount(posicao.Valor, posicao.Quantidade, initCdi.Valor, currentCdi.Valor, diasUteis);
                var resultado = new PocCalculo
                {
                    IdPosicao = posicao.Id,
                    TipoCalculo = 3,
                    Conta = posicao.Conta,
                    Valor = calculo
                };
                await _calculoRepository.AdicionarAsync(resultado);
            });
            await Task.WhenAll(tasks);

            stopwatch.Stop();
            var tempo = stopwatch.Elapsed;
            var duracao = tempo.ToString("mm':'ss':'fff");
            var execucao = new PocExecucao()
            {
                TipoCalculo = 3,
                Tempo = duracao,
                Data = DateTime.Now
            };
            await _execucaoRepository.AdicionarAsync(execucao);
        }
    }
}
