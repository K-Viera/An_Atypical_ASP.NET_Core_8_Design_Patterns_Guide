using Microsoft.AspNetCore.Mvc;
using Strategy.Data;
using Strategy.Services;

namespace Strategy.Controllers;

[Route("travel/[controller]")]
[ApiController]
public class ControlFreakLocationsController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<LocationSummary>> GetAsync(CancellationToken cancellationToken)
    {
        var locationService = new InMemoryLocationService();
        var locations = await locationService.FetchAllAsync(cancellationToken);
        return locations.Select(l => new LocationSummary(l.Id, l.Name));
    }
}

[Route("travel/[controller]")]
[ApiController]
public class ControlFreakUpdatedLocationsController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<LocationSummary>> GetAsync(CancellationToken cancellationToken)
    {
        var database = new NotImplementedDatabase();
        var locationService = new SqlLocationService(database);
        var locations = await locationService.FetchAllAsync(cancellationToken);
        return locations.Select(l => new LocationSummary(l.Id, l.Name));
    }

}
