var FavoriteVM = function (favorite) {
    var self = this;

    self.favoriteId = favorite?.id;
    self.hasFavorites = favorite != null && favorite.players.length > 0;
    self.players = ko.observableArray();
    self.dirty = ko.observable(false);

    self.buttonStyle = ko.computed(function () {
        return self.dirty() ? "btn btn-primary" : "btn btn-outline-primary";
    }, this);

    if (self.hasFavorites) {
        favorite.players.forEach(function (player) {
            self.players.push(new Player(player));
        });
    }

    self.updateFavRank = function (id, favRank) {
        var fav = ko.utils.arrayFirst(self.players(), function (currentPlayer) {
            return currentPlayer.id() == id;
        });

        if (fav) {
            fav.favRank(favRank);
            self.dirty(true);
        }
    }

    self.updateFavorites = function () {
        self.dirty(false);

        $.ajax({
            type: "POST",
            url: "/Favorites/UpdateFavorites",
            data: { players: ko.toJS(self.players), favoriteId: self.favoriteId },
            dataType: "json"
        }).done(function (response) {
            if (response == true) {
                $.notify('Favorite players updated', 'success', { position: 'bottom left' });
            }
            else {
                $.notify("Favorite players update failed!", "error");
            }
        }).fail(function (response) {
            $.notify("Favorite update failed!", "error");
        }).done(function () {
        });
    }
};


function Player(player) {
    var self = this;

    self.favRank = ko.observable(player.favRank);
    self.id = ko.observable(player.id);
    self.atpRank = player.rank;
    self.country = player.countryName;
    self.age = player.ageAtRankDate;
    self.points = player.points;
    self.firstName = player.firstName;
    self.lastName = player.lastName;
    self.picUrl = player.playerHeadshotImageCmsUrl;
}

