//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Produtoes
    {
        [DataMember]
        public int ProdutoId { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public decimal Preco { get; set; }

        [DataMember]
        public System.DateTime UltimaCompra { get; set; }

        [DataMember]
        public float Estoque { get; set; }

        [DataMember]
        public string Comentario { get; set; }

        [DataMember]
        public Nullable<float> Quantidade { get; set; }

        [IgnoreDataMember]
        public string Discriminator { get; set; }
    }
}
