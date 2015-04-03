$(document).ready(function () {
    //$('#search-data-input').keyup(function () {
    //    var query = $(this).val().trim();
    //    if (query.length >= 4) {
    //        $.ajax({
    //            type: 'GET',
    //            url: '/Search/Index',
    //            data: { query: query },
    //            success: function (data, success) {
    //                $("#tags-dropdown").fillSelect(data).attr('size',6);
    //                console.log(data);
    //            },
    //            error: function (data, error) {
    //                console.log(data);
    //            }
    //        })
    //    }
    //})

    $('.subcategory-list-item-checkbox').change(function () {
        var idSelector = function () { return this.id.split('-')[1]; };
        var allCheckedSubCategoryIds = $('.subcategory-list-item-checkbox:checked').map(idSelector).get();
        if (allCheckedSubCategoryIds.length === 0) {
            $('.model-item').show();
        }
        else {
            $('.model-item').hide();
            allCheckedSubCategoryIds.forEach(function (element, index) {
                $('.model-subcategory-' + element).show();
            })
        }
        var countShownModels = $('.model-item:visible').size();
        $('#models-counter').text(countShownModels);
    })
})

$.fn.clearSelect = function () {
    return this.each(function () {
        if (this.tagName === 'SELECT')
            this.options.length = 0;
    });
}

$.fn.fillSelect = function (data) {
    return this.clearSelect().each(function () {
        if (this.tagName === 'SELECT') {
            var dropdownList = this;
            $.each(data, function (index, optionData) {
                var option = new Option(optionData.Text, optionData.Value);


                dropdownList.add(option, null);

            });
        }
    });
}