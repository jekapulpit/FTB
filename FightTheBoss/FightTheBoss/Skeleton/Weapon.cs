﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    public abstract class Weapon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int damage { get; set; }
        public string Call { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }
        public User User { get; set; }

        [ForeignKey("Fighter")]
        public int? FighterId { get; set; }
        public Fighter Fighter { get; set; }

        public Weapon()
        {
            
        }

        public Weapon(int damage)
        {

            this.damage = damage;
        }
        public abstract void Attackeffect(Fighter Goal);
    }
}
