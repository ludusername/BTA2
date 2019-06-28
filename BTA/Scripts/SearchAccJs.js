$("#searchInput1").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Account/GetSearchValue1',
            dataType: "json",
            data: { search: $("#searchInput1").val() },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.Name, value: item.Name };
                }));


            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});