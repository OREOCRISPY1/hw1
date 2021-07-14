﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Cast
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public String Name { get; set; }
        public String Gender { get; set; }
        public String TmdbUrl { get; set; }
        [MaxLength(2084)]
        public String ProfilePath { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; }
    }
}
