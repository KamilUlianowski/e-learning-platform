$(".movieRow").hover(function () {
    $(this).toggleClass('active');
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

$(".pathLi").click(function() {
    $(".pathLi").removeClass("selected");
    $(this).addClass("selected");
    $(".courseDiv").hide();
    $("." + $(this).text()).show();
    if ($(this).text() == "All") {
        $(".courseDiv").show();
    }
});

$("#LiMovies").click(function () {
    $(".li-movies").removeClass("selected");
    $(this).addClass("selected");
    $(".section-content-course").hide();
    $("#TableOfContent").show();
});

$("#LiDescription").click(function () {
    $(".li-movies").removeClass("selected");
    $(this).addClass("selected");
    $(".section-content-course").hide();
    $("#Description").show();
});

$("#LiDiscussion").click(function () {
    $(".li-movies").removeClass("selected");
    $(this).addClass("selected");
    $(".section-content-course").hide();
    $("#Discussion").show();
});


$("#LiTest").click(function () {
    $(".li-movies").removeClass("selected");
    $(this).addClass("selected");
    $(".section-content-course").hide();
    $("#LearningCheck").show();
});