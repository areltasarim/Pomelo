﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace EticaretWebCoreEntity.Infrastructure
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }


        public abstract void Build(ModelBuilder builder);


        


        //public T FromDil<T>(string dil = "tr-TR",int id=0)
        //{
        //    T Snc = GetFromDatabase<T>(id);
        //    return Snc(p => p.Diller.DilKodlari.DilKodu == dil);
        //}

    }
}