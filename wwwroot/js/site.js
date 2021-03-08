// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// disable modal to open when favorites checkbox is clicked also send model to controller
$('.modal-no-open').on('click', function (event) {

    var playerData = $(event.currentTarget).parent().data('bind');
    var isChecked = $(event.currentTarget).find('input').is(':checked');
    event.stopPropagation();

    $.ajax({
        type: "POST",
        url: "/Players/UpdateFavorite",
        data: { player: playerData, selected: isChecked },
        dataType: "json"
    }).done(function (response) {
        if (response == true) {
            $.notify(isChecked ? 'Favorite added' : 'Favorite removed', 'success');
        }
        else {
            $.notify("Favorite update failed!", "error");
        }
    }).fail(function (response) {
        $.notify("Favorite update failed!", "error");
    }).done(function () {
    });
});


$('#playerModal').on('show.bs.modal', function (event) {
    // hide the modal content before data is loaded so it doesnt look broken
    $('#bioDiv').hide();
    $('#spinner').show();

    var rowClicked = $(event.relatedTarget);
    var id = rowClicked.data('bind').PlayerId;

    $.ajax({
        url: '/Players/PlayerBio',
        type: "GET",
        contentType: "json",
        data: { playerId: id },
        success: function (data) {
            $('.modal-title').text(data.data.firstName + " " + data.data.lastName + "' Biography (Ranked #" + data.data.heroRank + ")");
            $('#playerBio').html(data.data.bioPersonal);
            $('#playerBioYearToDate').html(data.data.bioYearToDate);
            $('#playerCareerHighlights').html(data.data.bioCareerHighlights);
            $('#playerImage').attr('src', 'https://www.atptour.com/' + data.content.playerProfileDetails.playerGladiatorUrl);
        },
        error: function (xhr) {
            console.log("failed to load modal for Id:" + id);
        }
    }).done(function () {
        $('#bioDiv').show();
        $('#spinner').hide();
    });
})

