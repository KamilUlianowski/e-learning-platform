$(".movieRow").hover(function () {
    $(this).toggleClass('active');
});

$("#ButtonPosts").click(function () {
    $("#Posts").toggle();
});

$(".rateit").click(function () {
    var help = $(this).rateit('value');
    var movieId = $(this).attr("id");
    $(this).rateit('readonly', true);
    $.ajax({

        type: "POST",
        url: "/Course/AddVote",
        data: { movieId: movieId, rating: help },
        success: function (msg) {
        },
        error: function (e) {
        }
    });

});

$("#sidebar").css("width", 60);
$("#sidebar").hover(
    function () {
        $(this).animate({ width: '200' }, 200);
        $(".itemText").show();
    },
    function () {
        $(this).animate({ width: '60px' }, 200);

    }

);