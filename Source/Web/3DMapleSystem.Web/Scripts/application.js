$(document).ready(function () {
    //$('#btn-download').confirm();

    //$(document).on('click', '.btn-download-model', function () {
    //    var $that = $(this);
    //    $that.confirm({
    //        confirm: function () {
    //            $.ajax({
    //                url: $that.href,
    //                method: "GET",
    //                data: [],
    //                success: function () {
    //                    $.ajax({
    //                        url: "/PolyModels/DownloadSuccess",
    //                        method: "POST",
    //                        data: { modelId: $that.attr('id') },
    //                        success: function (data) {
    //                            $('#download-limits-wrapper').html(data);
    //                        }
    //                    })
    //                }
    //            })
    //        }
    //    })
    //})

    $("#dialog-confirm").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        height: 180,
    });

    $(".downloadLink").click(function (e) {
        e.preventDefault();
        var targetUrl = $(this).attr("href");
        var modelId = ($(this).attr("id")).split("_")[1];
        var rank = $(this).attr("rank");

        $("#dialog-confirm").dialog({
            buttons: {
                "Confirm": function () {
                    $(this).dialog("close");
                    $.ajax({
                        url: targetUrl,
                        method: 'GET',
                        data: [],
                        success: function () {
                            window.location.href = targetUrl;
                            $.ajax({
                                url: "/PolyModels/DownloadSuccess",
                                method: 'POST',
                                data: { modelId: modelId },
                                success: function (data) {
                                    $('#download-limits-wrapper').html(data);
                                }
                            })
                        },
                        error: function () {
                            $('.limits-error').find('.rank-name').addClass("rank-name-" + rank).text(rank);
                            $('.limits-error').show();
                        }
                    })
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#dialog-confirm").dialog("open");
    });

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

    //$('.rowspan').each(function () {
    //    var that = $(this);
    //    var classNames = $(this).attr('class').split(' ');
    //    $.each(classNames, function (index, item) {
    //        // Find class that starts with btn-
    //        if (item.indexOf("rowspan-") === 0) {
    //            var numberOfRows = item.split('-')[1];
    //            console.log(that);
    //            var template = that.next().find('.col-template').first();
    //            var templateHeight = template.height();
    //            var height = numberOfRows * templateHeight;
    //            console.log(height);
    //            console.log(that);
    //            // Store it
    //            that.height(height);
    //        }
    //    });
    //})
})

function successfulDownload(modelId) {
    $.ajax({
        url: "/PolyModels/DownloadSuccess",
        method: "POST",
        data: { modelId: modelId },
        success: function (data) {
            $('#download-limits-wrapper').html(data);
        }
    })
}

//function downloadModel(url, modelId) {
//    var $that = $(this);
//    if (confirm("Are you sure you want download this model")) {
//        $.ajax({
//            url: $that.href,
//            method: "GET",
//            data: [],
//            success: function () {
//                $.ajax({
//                    url: url,
//                    method: "POST",
//                    data: { modelId: modelId },
//                    success: function (data) {
//                        $('#download-limits-wrapper').html(data);
//                    }
//                })
//            }
//        })
//    }
//}

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