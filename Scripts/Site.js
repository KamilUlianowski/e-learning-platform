

function onLoginBegin() {
    $("#modal-overlay").show();
    $(".spin").show();
}

function onLoadEnd() {
    $("#modal-overlay").hide();
    $(".spin").hide();
}

if ($(window).width() < 1600) {
    $("#SectionImage").addClass("col-lg-10 col-lg-offset-1");
    $("#SectionMovies").addClass("col-lg-10 col-lg-offset-1");
} else {
    $("#SectionImage").addClass("col-lg-8 col-lg-offset-2");
    $("#SectionMovies").addClass("col-lg-8 col-lg-offset-2");
}

$(".movieRow").hover(function () {
    $(this).toggleClass('active');
});

$("#Sidebar").css("width", 60);
$("#Sidebar").hover(
    function () {
        $(this).animate({ width: '200' }, 200);
        $(".item-text").show();
    },
    function () {
        $(this).animate({ width: '60px' }, 200);

    }

);

$(".search-icon").click(function() {
    window.location.href = "/Course/Search?searchedText=" + $("#SearchInput").val();
});

$("#SearchInput").keypress(function (e) {
    if (e.which == 13) {
        window.location.href = "/Course/Search?searchedText=" + $("#SearchInput").val();
    }
});

$(".pathLi").click(function () {
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
    $("#Answer0").prop("checked", true);
});
$('div[title=1]').click(function () {
    $("#Answer1").prop("checked", true);
});

$('div[title=2]').click(function () {
    $("#Answer2").prop("checked", true);
});

function validateYouTubeUrl() {
    var url = $('.youtube-url').val();
    if (url != undefined || url != '') {
        var regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=|\?v=)([^#\&\?]*).*/;
        var match = url.match(regExp);
        if (match && match[2].length == 11) {
            $(".youtube-url-validator").text("");
            $('.add-movie-button').attr("disabled", false);
        } else {
            $(".youtube-url-validator").text("This is not valid youtube url");
            $('.add-movie-button').attr("disabled", true);
        }
    }
}
