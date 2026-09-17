using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inkukan.Api.Conventions;

public class RemoveConsumesForDeleteConvention : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        bool isDelete = action.Selectors
            .SelectMany(s => s.ActionConstraints ?? Enumerable.Empty<IActionConstraintMetadata>())
            .OfType<HttpMethodActionConstraint>()
            .Any(c => c.HttpMethods.Contains(HttpMethod.Delete.Method));

        if (isDelete)
        {
            List<IFilterMetadata> toRemove = action.Filters.Where(f => f is ConsumesAttribute).ToList();
            foreach (IFilterMetadata filter in toRemove)
            {
                action.Filters.Remove(filter);
            }
        }
    }
}