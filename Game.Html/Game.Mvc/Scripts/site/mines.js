
$(document).ready(function () {
    var updateResources = function () {
        updateResource("Clay");
        updateResource("Wood");
        updateResource("Wheat");
        updateResource("Iron");
    };

    var updateResource = function (resourceName) {
        var currentProduction = 0;
        var start = new Date();
        var lastUpdated = $(".res-update." + resourceName).text();
        var lastDateUpdated = Date.parse(lastUpdated, "yyyy-MM-dd HH:mm:ss");
        var mines = $(".mines").find("." + resourceName);

        $.each(mines, function (index, value) {
            currentProduction += parseInt($(value).find(".hourProduction").text());
        })

        var currentValue = parseFloat($(".res-value." + resourceName).text());
        var nextValue = (currentValue + ((start.getTime() - lastDateUpdated) / 1000 / 60 / 60) * currentProduction).toFixed(4);

        $(".res-value." + resourceName).text(nextValue + " ");
        $(".res-update." + resourceName).text(start.strftime("%Y-%m-%d %H:%M:%S"));
    };

    // setInterval(updateResources, 500);


    var updateAjaxResources = function () {
        $.ajax({
            type: 'GET',
            url: "/Mines/Resources",
            success: function (data) {
                $.each(data, function (index, value) {
                    $(".res-value." + value.Type).text(value.Level.toFixed(4));
                });
            }
        });
        // $(“#container”).load(“/file.html”);


    };

    //setInterval(updateAjaxResources, 500);

    //exercitiu 1
    var getMineDetailsHTML = function (mineId) {
        $('#mine-details-container > .content').empty();
        $('#mine-details-container > .content').load("/Mines/Details?mineId=" + mineId);
        $('#mine-details-container').addClass('show');
    };

    $('.mine-details-btn').click(function (e) {
        var mineId = $(this).data('mine-id');
        getMineDetailsHTML(mineId);
    });

    //Exercitiu 2
    $('#mine-details-container > .close-btn').click(function () {
        $('#mine-details-container').removeClass('show');
    });

    //exercitiu 3
    $('#mine-details-container').on('click', '.upgrade-mine', function (e) {
        console.log('click');
        $.post('/Mines/Upgrade',
            {
                mineId: $(this).data('mine-id'),
                fastUpgrade: false
            },
            function (response) {
                console.log(response);
        });
    });

});