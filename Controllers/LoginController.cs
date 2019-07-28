using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLoginApi.BlockChain;

namespace WebLoginApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        // GET: api/AccountSearch
        [HttpGet]
        public string Get()
        {
            return "Login";
        }

        [HttpGet("{id}", Name = "Get")]
        public Block Get(string id)
        {
            foreach (Block I in Program.Chain.Chain)
            {
                var UsernameInDB = I.Username;
                if (id == UsernameInDB)
                {
                    new SaveBlockChain();
                    return I;
                }
            }
            return new Block(DateTime.Now, null, null, null, null, 1);
        }


        [HttpPost]
        public Block Post([FromBody] Block value) //Block(DateTime.Now, "wNMItkPPNtLQioqJsuut7+z4FF2Jv6KC6HHz9Ss9MGE=", "Reece", "Reece123", null, 1);
        {
            if (value != null)
            {
                string Inputted = value.Password;
                Blockchain Chain = Program.Chain;
                foreach (Block I in Chain.Chain)
                {
                    if(I.Username == value.Username)
                    {
                        string Password4Account = Encrypt.DecryptString(I.Password, I.PreviousHash + I.Username + value.Password);

                        if (Password4Account == value.Password)
                        {
                            return value;
                        }
                    }
                }
            }
            return new Block(DateTime.Now, null, null, null, null, 1);
        }

    }
}
