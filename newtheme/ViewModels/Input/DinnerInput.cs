using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Omu.AwesomeMvc;

namespace EDI.ViewModels.Input
{
    public class DinnerInput
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [UIHint("MultiLookup")]
        public IEnumerable<int> Meals { get; set; }

        [UIHint("Lookup")]
        [Required]
        public int? Chef { get; set; }

        [Required]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetAllMeals", Controller = "Data")]
        [DisplayName("Bonus meal")]
        public int? BonusMealId { get; set; }
    }
}