var waypoint = new Waypoint({
    element: document.getElementById('js--tab'),
    handler: function (direction) {
        $('.tab').addClass('sticky');
    }

});