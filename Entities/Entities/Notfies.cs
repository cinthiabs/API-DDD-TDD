﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notifies
    {
        public Notifies()
        {
            Notitycoes = new List<Notifies>();
        }
        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string Mensagem { get; set; }
        [NotMapped]
        public List<Notifies> Notitycoes { get; set; }

        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }
            return true;
        }
        public bool ValidaPropriedadeInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notitycoes.Add(new Notifies
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }
            return true;
        }
    }
}
