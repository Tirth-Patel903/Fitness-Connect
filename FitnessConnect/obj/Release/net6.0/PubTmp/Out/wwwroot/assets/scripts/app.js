$(document).ready(function () {
    $(".sidebar-toggler").click(function () {
        $("body").toggleClass("page-sidebar-closed");
    });

    $(".navbar-toggle").click(function () {
        $(".page-sidebar").toggleClass("open");
    });

    //Navbar and Sidebar items responsiveness...
    if ($(window).width() <= 767) {
        $("#nav-items-navbar").hide();
    }
    if ($(window).width() >= 767) {
        $("#nav-items-sidebar").hide();
    }
    $(window).resize(function () {
        if ($(window).width() <= 767) {
            $("#nav-items-navbar").hide();
        }
        else {
            $("#nav-items-navbar").show();
        }
        if ($(window).width() >= 767) {
            $("#nav-items-sidebar").hide();
        }
        else {
            $("#nav-items-sidebar").show();
        }
    });
});
