using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetFundamentalDemo.DTOs
{
    public record ChampionDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Ultimate { get; set; }
    }
}
