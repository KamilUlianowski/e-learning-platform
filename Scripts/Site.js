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
