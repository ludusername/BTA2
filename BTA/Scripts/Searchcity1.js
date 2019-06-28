$("#searchInput1").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Account/GetSearchValue',
            dataType: "json",
            data: { search: $("#searchInput1").val() },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.CityName, value: item.CityName };
                }));


            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});