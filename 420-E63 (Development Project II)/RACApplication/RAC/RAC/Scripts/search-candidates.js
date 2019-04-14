$(document).ready(function() {
    const downArrow = '&#x25BE;';
    const upArrow = '&#x25B4;';
    search();
    sortByLastNameDescending();
    $('#search').on('click', search);
    $('.searchCondition').on('change', search);

    $('#nameCol').on('click',
        function () {
            setNameSortedHeaderClass();
            if ($('#table').attr('sortby') === 'lastname') {
                if ($('#table').attr('orderin') === 'desc') {
                    sortByLastNameAscending();
                } else {
                    sortByLastNameDescending();
                }
            } else {
                sortByLastNameDescending();
            }
        });

    $('#dateStartCol').on('click',
        function () {
            setDateSortedHeaderClass();
            if ($('#table').attr('sortby') === 'datestart') {
                if ($('#table').attr('orderin') === 'desc') {
                    sortByStartDateAscending();
                } else {
                    sortByStartDateDescending();
                }
            } else {
                sortByStartDateAscending();
            }
        });

    function search() {
        setNameSortedHeaderClass();
        var firstName = $('#firstName')[0].value || '';
        var lastName = $('#lastName')[0].value || '';
        var racStatus = $('#racStatus').val() || '-1';

        $.each($('.candidate').toArray(),
            function(index, item) {
                var filterOut = false;

                if (firstName !== '') {
                    firstName = firstName.toLocaleLowerCase();
                    $(item).attr('firstname', $(item).attr('firstname').toLocaleLowerCase());
                    if (!($(item).attr('firstname').includes(firstName))) {
                        filterOut = true;
                    }
                }

                if (lastName !== '') {
                    lastName = lastName.toLocaleLowerCase();
                    $(item).attr('lastname', $(item).attr('lastname').toLocaleLowerCase());
                    if (!($(item).attr('lastname').includes(lastName))) {
                        filterOut = true;
                    }
                }

                if ($(item).attr('archived') === 'True') {
                    if (racStatus !== '-2') {
                        filterOut = true;
                    }
                } else if ($(item).attr('deleted') === 'True') {
                    if (racStatus !== '-3') {
                        filterOut = true;
                    }
                } else if (racStatus !== '-1') {
                    if ($(item).attr('racstatus') !== racStatus) {
                        filterOut = true;
                    }
                }

                if (!filterOut) {
                    $(item).addClass('resultIncluded').removeClass('resultExcluded');
                } else {
                    $(item).addClass('resultExcluded').removeClass('resultIncluded');
                }
            }
        );
    }

    function sortByLastNameDescending() {
        const array = $('.candidate').toArray();
        array.sort(function(a, b) {
            return $(a).attr('lastname').localeCompare($(b).attr('lastname'));
        });
        $('#table').append(array);
        $('#table').attr('sortby', 'lastname');
        $('#table').attr('orderin', 'desc');
        $('#nameColArrow').html(downArrow);
    }

    function sortByLastNameAscending() {
        const array = $('.candidate').toArray();
        array.sort(function(a, b) {
            return $(b).attr('lastname').localeCompare($(a).attr('lastname'));
        });
        $('#table').append(array);
        $('#table').attr('sortby', 'lastname');
        $('#table').attr('orderin', 'asce');
        $('#nameColArrow').html(upArrow);
    }

    function sortByStartDateDescending() {
        const array = $('.candidate').toArray();
        array.sort(function(a, b) {
            return Date.parse($(a).attr('date')) - Date.parse($(b).attr('date'));
        });
        $('#table').append(array);
        $('#table').attr('sortby', 'datestart');
        $('#table').attr('orderin', 'desc');
        $('#dateStartColArrow').html(downArrow);
    }

    function sortByStartDateAscending() {
        const array = $('.candidate').toArray();
        array.sort(function(a, b) {
            return Date.parse($(b).attr('date')) - Date.parse($(a).attr('date'));
        });
        $('#table').append(array);
        $('#table').attr('sortby', 'datestart');
        $('#table').attr('orderin', 'asce');
        $('#dateStartColArrow').html(upArrow);
    }

    function removeNameSortedClass() {
        $('#nameCol').removeClass('columnSorted');
        $('.nameCells').removeClass('columnSorted');
    }

    function removeDateSortedClass() {
        $('#dateStartCol').removeClass('columnSorted');
        $('.dateCells').removeClass('columnSorted');
    }

    function setDateSortedHeaderClass() {
        $('#dateStartCol').addClass('columnSorted');
        setDateSortedCellClass();
        removeNameSortedClass();
    }

    function setNameSortedHeaderClass() {
        $('#nameCol').addClass('columnSorted');
        setNameSortedCellClass();
        removeDateSortedClass();
    }

    function setNameSortedCellClass() {
        $('.nameCells').addClass('columnSorted');

    }

    function setDateSortedCellClass() {
        $('.dateCells').addClass('columnSorted');
    }
});