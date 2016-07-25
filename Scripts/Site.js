$(".movieRow").hover(function () {
    $(this).toggleClass('active');
});

$("#sidebar").css("width", 60);
$("#sidebar").hover(
    function () {
        $(this).animate({ width: '200' }, 200);
        $(".item-text").show();
    },
    function () {
        $(this).animate({ width: '60px' }, 200);

    }

);

$(".pathLi").click(function() {
    $(".pathLi").removeClass("selected");
    $(this).addClass("selected");
    $(".course-div").hide();
    $("." + $(this).text()).show();
    if ($(this).text() == "All") {
        $(".course-div").show();
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

$('div[title=0]').click(function () {
    $("#answer0").prop("checked", true);
});
$('div[title=1]').click(function() {
    $("#answer1").prop("checked", true);
});

$('div[title=2]').click(function() {
    $("#answer2").prop("checked", true);
});

$window.resize(function() {
    if ($window.width() < 514) {
        return $html.addClass('mobile');
    }
});