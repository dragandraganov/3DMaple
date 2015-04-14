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

    //Rating implementation section
    $(document).on('mouseenter', ".rating-star", function () {
        $('.rating-star').removeClass('filled');
        $(this).prevUntil(".rating-area").addClass('filled');
        $(this).addClass('filled');
        $(this).attr('title', 'Click to rate: ' + $(this).next('input').val());
        $('.rating-dynamic-value').text($(this).next('input').val());
    })

    $(document).on('mouseleave', ".rating-star", function () {
        returnUserRating();

        //if ($(this).parent().find(".rating-star").hasClass('selected')) {
        //    var selected = $(this).parent().find('.selected').first();
        //    selected.nextUntil('input :submit').removeClass('filled')
        //}
        //else {
        //    $(this).prevUntil(".rating-area").removeClass('filled');
        //    $(this).removeClass('filled');
        //}
    })

    $(document).on('click', ".rating-star", function (data, status, xhr) {
        var checkedStar = $(this).next('input');
        var checkedValue = parseInt(checkedStar.val());
        var modelId = window.location.href.split("/")[4];
        $.ajax({
            url: "/ratings/vote?modelId=" + modelId + "&rating=" + checkedValue,
            method: "POST",
            data: AddAntiForgeryToken({}),
            success: function (data, status, xhr) {
                if (xhr.getResponseHeader("X-Responded-JSON")) {
                    if ($.parseJSON(xhr.getResponseHeader("X-Responded-JSON")).status === 401) {
                        window.location.href = "/Account/Login";
                    }
                }
               
                else {
                    $('input[name="rating"]').prop('checked', false);
                    checkedStar.prop('checked', true);
                    $('.rating-star').removeClass('filled');
                    $('.rating-star').slice(0, checkedValue).each(function () {
                        $(this).addClass('filled');
                    });
                    $('.rating-dynamic-value').text(checkedStar.val());
                    $('#rating-result').html(data);
                }
            }
        })
    })

    $(document).on('click', '.btn-clear-rating', function () {
        if ($('input[name="rating"]:checked').length > 0) {
            var modelId = window.location.href.split("/")[4];
            $.ajax({
                url: "/ratings?modelId=" + modelId,
                method: "POST",
                success: function (data) {
                    $('#rating-result').html(data);
                }
            })
        }
        $('.rating-star').removeClass('filled');
        $('input[name="rating"]').prop('checked', false);
        $('.rating-dynamic-value').text('-');
    })

    $(document).ajaxError(function (event, jqxhr, settings, exception) {
        if (jqxhr.status === 401) {
            // unauthorized
            window.location.href = '/Account/Login';
        }
    });
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

function displayCurrentUserRating(value) {
    $('input[name="rating"][value="' + value + '"]').attr('checked', true);
    returnUserRating();
}

function returnUserRating() {
    var checkedStar = $('input[name="rating"]:checked');
    var checkedValue = parseInt(checkedStar.val());
    $('.rating-star').removeClass('filled');
    if (checkedValue > 0) {
        $('.rating-star').slice(0, checkedValue).each(function () {
            $(this).addClass('filled');
        });
        $('.rating-dynamic-value').text(checkedStar.val());
    }
    else {
        $('.rating-dynamic-value').text('-');

    }
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

AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};