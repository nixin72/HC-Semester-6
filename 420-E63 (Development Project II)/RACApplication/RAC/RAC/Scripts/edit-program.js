$(document).ready(function () {
    resetLinks();
    resetNames();


    function edit(context) {
        changePopupHeader('Edit Element');
        $('#NewEntry').detach().appendTo('#popupContent');
        $('#NewEntry').removeClass('hidden');

        var description = $(context).parent().parent().find('.descriptionInput').val();
        $.validator.unobtrusive.parse('#ElementField');

        $('#Description').val(description);
        $('#Add').hide();
        $('#Edit').show();
        showPopup();
        $('#Edit').off();
        $('#Edit').click(function () {
            $('#ElementField').validate();
            $('#ElementField').validate().element('#Description');
            if ($('#ElementField').valid()) {
                console.log();
                $(context).parent().parent().find('.descriptionDisplay').text($('#Description').val());
                $(context).parent().parent().find('.descriptionInput').val($('#Description').val());
                hidePopup();
                resetLinks();
                $('#NewEntry').detach().appendTo('#FormHolder');
                $('#NewEntry').addClass('hidden');
            }
        });
    }

    function add(context) {
        var links = '<a class="edit" href="javascript:;">Edit</a> | <a class="remove" href="javascript:;">Remove</a>';
        
        changePopupHeader('Add Element');

        $('#NewEntry').detach().appendTo('#popupContent');
        $('#NewEntry').removeClass('hidden');
        var addLink = context;
        $.validator.unobtrusive.parse('#ElementField');
        $('#Description').val('');
        $('#Edit').hide();
        $('#Add').show();
        showPopup();
        $('#Add').off();
        $('#Add').click(function () {
            $('#ElementField').validate();
            $('#ElementField').validate().element('#Description');
            if ($('#ElementField').valid()) {
                var hiddenInput = '<input type="hidden" class="descriptionInput" value="' + $('#Description').val() + '"/>';

                if ($(context).parent().parent().parent().find('div.placeholder').length === 0) {
                    console.log('Add to the elements');
                    var entry = '<div class="element"><div class="descriptionCell">' + hiddenInput + '<span class="descriptionDisplay"> ' + $('#Description').val() + '</span>' + '</div><div class="optionsCell">' + links + '</div></div>';
                    $(entry).insertBefore($(context).parent().parent().parent().find('div.addNewElementCell'));

                }
                else {
                    console.log('replace the placeholder');
                    ($(context).parent().parent().parent().find('div.placeholder').html('<div class="descriptionCell">' + hiddenInput + '<span class="descriptionDisplay"> ' + $('#Description').val() + '</span>' + '</div><div class="optionsCell">' + links + '</div>'));
                    $(context).parent().parent().parent().find('div.placeholder').addClass('element');
                    $(context).parent().parent().parent().find('div.placeholder').removeClass('placeholder');

                }


                hidePopup();
                resetLinks();
                resetNames();

                $('#NewEntry').detach().appendTo('#FormHolder');
                $('#NewEntry').addClass('hidden');
            }
        });
    }

    function remove(context) {
        var description = $(context).parent().parent().find('.descriptionInput').val();
        $('#NewEntry').detach().appendTo('#FormHolder');
        $('#NewEntry').addClass('hidden');
        changePopupHeader('Are you sure you want to delete?');
        changePopupContent('<div><p>Element: ' + description + "</p></div><button id='Delete' class='btn btn-default'>Confirm</button>");
        showPopup();
        $('#Delete').click(function () {
            //Create place holder element if it is the last element of the competency that you wanted to remove
            if ($(context).parent().parent().parent().find('div.element').length < 2) {
                var entry = '<span class="descriptionDisplay">There are no competency elements in this competency</span>';
                console.log(context);

                console.log(context.parent().parent());
                
                context.parent().parent().removeClass('element');
                context.parent().parent().addClass('placeholder');
                context.parent().parent().html(entry);
            }
            else
                //just remove the element
            {
                context.parent().parent().remove();
            }

            hidePopup();
            resetLinks();
            resetNames();
            changePopupContent(' ');

        });
    }

    function resetLinks() {
        $('.edit').click(function () {
            edit($(this));
        });

        $('.add').click(function () {
            add($(this));
        });

        $('.remove').click(function () {
            remove($(this));

        });
    }

    function resetNames() {
        var competencyIndex = -1;
        var elementIndex = 0;
        $('div').each(function (index, div) {


            if ($(div).hasClass('SetActiveComp')) {

                competencyIndex++;
                elementIndex = 0;
            }
            else if ($(div).hasClass('element')) {
                var name = 'Competencies[' + competencyIndex + '].CompetencyElements[' + elementIndex + '].ElementMinistryData.Description';
                $(div).children('div.descriptionCell').children('.descriptionInput').attr('name', name);
                elementIndex++;
            }
        });
    }

    $(document).on('click', '.SetActiveComp', function (e) {
        e.stopPropagation();
    });


})
