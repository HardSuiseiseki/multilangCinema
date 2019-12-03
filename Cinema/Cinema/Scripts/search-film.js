﻿$(document).ready(
    function() {
        var termInput = $(".js-search-film-term")[0];
        if (termInput === null || termInput === undefined) {
            return;
        }
        var searchResultTemplateContainer = $(".js-search-result-item")[0].innerHTML;
        var resultTemplate = Handlebars.compile(searchResultTemplateContainer);

        var pagingTemplateContainer = $(".js-search-page-item")[0].innerHTML;
        var pagingTemplate = Handlebars.compile(pagingTemplateContainer);
        var currentTerm = termInput.value;

        Handlebars.registerHelper('for',
            function(from, to, incr, block) {
                var accum = '';
                for (var i = from; i <= to; i += incr)
                    accum += block.fn(i);
                return accum;
            });


        $(".js-search-page-container").on('click',
            '.js-page-selector',
            function(e) {
                var targetElem = e.currentTarget;
                var dataSet = targetElem.dataset;
                searchFilm(dataSet.selectedPage);
            });

        $(".js-search-film-submit").on('click',
            function() {
                currentTerm = termInput.value;
                searchFilm(1);
            });

        function searchFilm(selectedPage) {
            var reqestModel = {
                term: currentTerm,
                currentPage: selectedPage
            };
            $.ajax({
                url: "SearchForFilms",
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                data: JSON.stringify(reqestModel)
            }).done(function(result) {
                var resultHtml = resultTemplate(result);
                $(".js-search-result-container").html(resultHtml);
                if (result.ShowPaging) {
                    var resultPaging = pagingTemplate(result);
                    $(".js-search-page-container").html(resultPaging);
                } else {
                    $(".js-search-page-container").html('');
                }
            }).fail(function() {
                alert("Search request processing failed. Please, contact system administrator.");
            });
        }
    });