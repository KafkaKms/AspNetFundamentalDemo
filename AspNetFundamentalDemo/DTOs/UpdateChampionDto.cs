using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.DTOs
{
    public record UpdateChampionDto
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Ultimate { get; set; }
    }
}
