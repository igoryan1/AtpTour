# AtpTour

ATP Assessment - a project that creates a simple web site that allows to view, sort and get bio details of tennis players, using a couple of API endpoints provided by the requirements.

## Getting Started

In the main/root folder run `RunApp.bat` file, once it finished building go to https://localhost:5001/ and the site should be up and running.

Alternatively, instance of this app was deployed to azure https://iztest.azurewebsites.net/

* *HOME* - Page (default) is just a static image, quite boring and no interaction
* *PLAYERS*  - Page initially loads top 10 ranked players. This page allows to:
    - Search for players by rank, using From and To ranks
	- Navigate between multiple pages if there are mora than 10 results
    - Select and deselect favorite players
    - View player's bio by clicking on the row
 * *FAVORITES* - Page loads all favorite players selected by the user on Players page. This page allows to:
    - Resort players by user's favorite rank and done by dragging/droppping rows in UI
    - Once players are sorted by favorite rank, use Update Favorites Button to save changes
 
### Considerations

- Database schema for storing favorite players is fairly simple, has `Favorite` table with ID and IP address. IP address is used as key to keep track of user's favorite players. This is probably not the best key to use, but for given assessment should do the job just fine. Second table is `Player` which has one-to-many relationship with `Favorite` table. Basically one `Favorite` record could have many `Player` records. 
- Player record is basically a full copy of player's details returned by one of the provided API endpoints. This was done so that player details can be stored since no endpoint was provided to pull a specific Player's details (bio endpoint only returns bio details), otherwise could only store PlayerId to re-inquire the details.
- Top ranked players are loaded directly from provided https://app.atptour.com/api/gateway/rankings.ranksglrollrange?fromrank=1&torank=10
- Paging records is added for more usability, its fairly simple that allows going Previous/Next. 
- `System.Net.Http.Json` a fairly new library used to easily map results from HttpClient to a speicific object, in this case `PlayerModel`.
- `System.Linq` library is heavily used throughout as it makes things very easy to work with.
- Bio for each player is cached in the system memory locally for 30 minutes, otherwise provided endpont is used to get the details https://app.atptour.com/api/gateway/players.PlayerProfileBio?playerid={playerId}
- Player's bio is loaded using Bootstrap's modal
- Both, loading bio and adding/removing favorite player by clicking a checkbox is done via Ajax call
- Players page is mostly built using Razor markup syntax
- Favorites page is built leveraging `Knockout.js` library. This is just to display different technology application.
- Sorting favorite players is done leveraging `sortable` functionality of `jQuery`
- Each favorite player has its own `Favorite Rank` field/column in DB to help keep track of the order.

### Prerequisites

Everything should already be included in the project, no additional installations needed.  For DB/SQL by default, LocalDB should create .mdf DB files in the C:/Users/<user> directory.
Below some of the packages and libraries added and used by the project.

```
Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
System.Net.Http.Json
https://cdnjs.cloudflare.com/ajax/libs/knockout/3.5.0/knockout-min.js
https://code.jquery.com/ui/1.12.1/jquery-ui.js
https://code.jquery.com/jquery-1.12.4.js
https://cdnjs.cloudflare.com/ajax/libs/notify/0.4.0/notify.js
```





## Authors

* *Igor Zimin* - *wants to work for AtpTour* 





