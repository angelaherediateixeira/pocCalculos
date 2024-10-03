using PocParalell.Domain;
using PocParalell.Extensions;
using PocParalell.Infrastructure.Interfaces;
using PocParalell.Services.Interfaces;
using System.Globalization;

namespace PocParalell.Services
{
    public class InsereService : IInsereService
    {
        private readonly ICdiRepository _repository;
        private readonly IPosicaoRepository _posicaoRepository;
        public InsereService(ICdiRepository repository, IPosicaoRepository posicaoRepository) 
        {
            _repository = repository;
            _posicaoRepository = posicaoRepository;
        }

        public async Task InsereCdis()
        {
            var rangeDays = 1095;
            var initData = DateTime.Now.AddDays(-1095);
            var valorBase = 5.0;
            var ponto = 0.00015;

            for (int i = 0; i < rangeDays; i++)
            {

                var cdi = new PocCdi()
                {
                    Valor = (decimal)(valorBase + ponto),
                    Data = initData.AddDays(i),
                };
                await _repository.AdicionarAsync(cdi);
                valorBase = (valorBase + ponto);
            }
        }

        public async Task InserePosicoes()
        {
            var rangeAccounts = 1000;
            var day = 1;
            var month = 1;
            var year = 2022;
            var initYear = 2022;
            var lastYear = 2023;
            var valorBase = 1.12003;
            var ponto = 0.00015;
            var quantidadeBase = 1000;
            var currentCdi = await _repository.BuscarAsync(x => x.Data.Date == DateTime.Now.Date.AddDays(-5));
            for (int i = 0; i < rangeAccounts; i++)
            {
                if (year == lastYear && month > 12)
                {
                    year = initYear;
                    month = 1;
                }
                if (day == 28)
                    day = 1;

                if (month == 13)
                    month = 1;

                var data = new DateTime(year, month, day).Date;
                var conta = (rangeAccounts + (i+1)).ToString().PadLeft(10, '0');
                var valor = (decimal)(valorBase + ponto);
                var quantidade = (quantidadeBase + i);
                var diasUteis = data.BusinessDaysUntil(DateTime.Now);
                var initCdi = await _repository.BuscarAsync(x => x.Data.Date == data);

                var calculo = CalculatorExtensions.CalculateAmount(valor, quantidade, initCdi.Valor, currentCdi.Valor, diasUteis);
                var posicao = new PocPosicao()
                {
                    Conta = conta,
                    Valor = (decimal)(valorBase + ponto),
                    Quantidade = (quantidadeBase + i),
                    ValorCalculado = (decimal)calculo,
                    Data = data,
                };
                await _posicaoRepository.AdicionarAsync(posicao);
                day++;

                if (day == 28)
                    month++;

                if (month == 12 && year == initYear && day == 28)
                    year++;

                valorBase = (valorBase + ponto);
            }
        }
    }
}
