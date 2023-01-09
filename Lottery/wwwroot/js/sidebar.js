var sideBarEnabled = false;

$("#options-icon").click(function () {
    if (!sideBarEnabled) {
        showSidebar();
    } else {
        hideSidebar();
    }
});

$("#screen-plug").click(function () {
    hideSidebar();
});

window.onresize = () => {
    if (sideBarEnabled) {
        hideSidebar();
    }
};

function showSidebar() {
    $("body").css({
        "overflow": "hidden"
    });

    $(".header").css({
        "background-color": "#D94E50",
        "flex-direction": "column",
        "align-items": "start",
        "justify-content": "start",
        "width": "250px",
        "height": "100vh",
        "position": "absolute"
    });

    $(".header .options").css({
        "flex-grow": "0"
    });     

    $("#screen-plug").css({
        "display": "block"
    });

    $("#tabs").css({
        "width": "100%",
        "margin-top": "8px",
        "height": "auto"
    });

    $("#tabs a").css({
        "margin": "2px 0"        
    });

    $(".stealthy").css({
        "display": "flex",
        "flex-direction": "column"        
    });  

    $(".header a").css({
        "text-align": "left",
    });

    $("#profile-container a").text("Профиль");

    $("#profile-container").css({
        "width": "100%"
    });

    $("#profile").css({
        "position": "relative"
    });

    $("#profile a").css({
        "width": "100%",
        "box-sizing": "border-box"
    });

    sideBarEnabled = true;
}

function hideSidebar() {    
    $("body").css({
        "overflow": ""
    });

    $(".header").removeAttr('style');

    $("#screen-plug").css({
        "display": "none"
    });

    $("#tabs a").css({
        "margin": "0",
        "margin-right": "5px"
    });
    
    $(".stealthy").removeAttr('style');
    $(".header a").removeAttr('style');
    $("#profile-container").removeAttr('style');
    $("#profile").removeAttr('style');
    $("#profile a").removeAttr('style');

    sideBarEnabled = false;
}