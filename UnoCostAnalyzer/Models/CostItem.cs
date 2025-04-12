using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoCostAnalyzer.Models;

public partial record CostItem(
    Guid Id,
    string? Description,
    decimal? Cost,
    string[] Tags,
    DateTime? CreatedAt)
{
    public CostItem() : this(Guid.NewGuid(), null, null, [], null) { }
};

