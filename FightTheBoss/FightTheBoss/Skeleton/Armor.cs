using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FightTheBoss.Skeleton
{
    public abstract class Armor : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ArmorPoints { get; set; }
        public string Call { get; set; }
        public bool IsWearing { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }
        public User User { get; set; }

        public string type { get; set; }

        public Armor(int armorpoints, string Name)
        {
            ArmorPoints = armorpoints;
            Call = Name;
            IsWearing = false;
        }
        public Armor() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
