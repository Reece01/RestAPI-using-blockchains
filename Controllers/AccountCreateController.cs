using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLoginApi.BlockChain;

namespace WebLoginApi.Controllers
{
    [Route("Login/[controller]")]
    [ApiController]
    public class Create : ControllerBase
    {
        // GET: api/LoginInfomation
        [HttpGet]
        public string Get()
        {
            return "Create";
        }

        // POST: api/LoginInfomation
        [HttpPost]
        public Block Post([FromBody] Block value)
        {
            if(value != null)
            {
                Blockchain Chain = Program.Chain;
                foreach (var I in Chain.Chain)
                {
                    if(I.Username == value.Username)
                        return new Block(DateTime.Now, null, null, null, null, 1);
                }
                Block Updated = new Block(DateTime.Now, null, value.Username, value.Password, value.Packeages, value.Pickle);
                Chain.AddBlock(Updated);
                new SaveBlockChain();
                return Updated;
            }
            return new Block(DateTime.Now, null, null, null, null,1);
        }
    }
}
