﻿using CsvHelper.Configuration;

namespace csv.Models
{
    public class ProdutoMap : ClassMap<Produto> 
    {
        public ProdutoMap() 
        {
            Map(p => p.Codigo).Name("id");
            Map(p => p.Descricao).Name("descricao");
            Map(p => p.Valor).Name("valor");
            Map(p => p.QuantidadeDisponivel).Name("quantidade");
        }
    }

}
