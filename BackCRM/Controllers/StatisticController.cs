using BackCRM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IConfiguration _config;
        public StatisticController(IConfiguration configuration)
        {
            _config = configuration;
        }
        [HttpGet]
        public dynamic getAmountPie(string? user)
        {
            List<Pie> pies = new();
            List<EMPL> emplList = new EMPLFactory(_config).getAll();
            List<Amount_D> _amount = new Amount_DFactory(_config).getAll();
            List<Budget> _budget = new BudgetFactory(_config).getAll();
            foreach (EMPL empl in emplList)
            {
                int payment = _amount.Where(x => x.empid == empl.EMPID && x.payment == "P").Select(x => x.amount).Sum();
                int notpayment = _amount.Where(x => x.empid == empl.EMPID && x.payment != "P").Select(x => x.amount).Sum();
                int budget = _budget.Where(x => x.empid == empl.EMPID).Select(x => x.budget).Sum();
                Pie p = new()
                {
                    empid = empl.EMPID,
                    emplname = empl.EMPNAME,
                    pay = payment,
                    nopay = notpayment,
                    nowtotal = budget + notpayment - payment,
                    total = budget+ notpayment,
                    root = new()
                    {
                        series = new()
                        {
                            new()
                            {
                                type = "pie",
                                label = new() { show = false },
                                data = new()
                                {
                                    new() { name = "採購總計", value = payment },
                                    new() { name = "剩餘預算", value = budget + notpayment - payment },
                                }
                            }
                        }
                    }
                };
                pies.Add(p);
            }
            pies = user == "A000" ?pies : pies.Where(x=>x.empid == user).ToList();
            string result = JsonConvert.SerializeObject(pies);
            return Ok(result);
        }
    }
}
