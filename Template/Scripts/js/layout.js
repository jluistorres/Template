toastr.options = { "progressBar": false, "positionClass": "toast-bottom-right", "closeButton": true, "showMethod": "slideDown" };
swal.setDefaults({ confirmButtonText: 'Aceptar', cancelButtonText: 'Cancelar' });

var Notificaciones = {
    hasValues: function (value) {
        $('.dropdown-list.notifications').removeClass('has-values').addClass(value > 0 ? 'has-values' : '');
    },
    WriteCookie: function (value) {
        $.cookie('notificaciones', value, { expires: 1, path: '/' });
        Notificaciones.hasValues(value);
    },
    ReadCookie: function () {
        var value = $.cookie('notificaciones') || 0;
        $('.dropdown-list.notifications .list-count').html(value);
        Notificaciones.hasValues(value);
    }
};

var AppLayout = function () {
    var handleBag = function () {
        //Mostrar items
        $(".header .dropdown-list").on("show.bs.dropdown", function (event) {
            var target = event.currentTarget;

            var numItems = $(target).find('.list-count');
            var bag = $(target).find('.dropdown-menu');
            bag.html('<li class="empty-list">Cargando...</li>');

            var url = bag.data('url') + 'BagList';
            $.get(url).done(function (html) {
                bag.html(html);
                //Layout.scroller();
                AppTheme.scroller();

                var countItems = bag.find('.dropdown-header').data('count') || 0;
                numItems.text(countItems);

                //Escribir en una cookie para notificaciones
                if ($(target).hasClass('notifications')) {
                    Notificaciones.WriteCookie(countItems);
                }
            });
        });

        //Eliminar item
        $('body').on('click', '.dropdown-list .media-delete', function () {
            var item = $(this).closest('.media');

            var isNotificacion = $(this).closest('.dropdown-list').hasClass('notifications');

            var bag = $(this).closest('.dropdown-list').find('.dropdown-menu');
            var url = bag.data('url') + 'BagRemove';

            $.post(url, { id: item.data('id') }, function (r) {
                var countItems = item.closest('.dropdown-list').find('.list-count');
                var newValue = parseInt(countItems.text()) - 1;
                countItems.text(newValue);

                if (!newValue) bag.parent().removeClass('open'); //bag.hide();
                else item.slideUp(200, function () { $(this).remove(); });

                if (isNotificacion) Notificaciones.WriteCookie(newValue);
            });
        });

        //Eliminar todos los items
        $('body').on('click', '.dropdown-list a.clear', function () {
            var cart = $(this).closest('.dropdown-list');
            var action = cart.find('.dropdown-menu').data('url') + 'BagRemoveAll'; //$(this).data('action');
            var isNotificacion = $(this).closest('.dropdown-list').hasClass('notifications');

            if (action) {
                swal({
                    title: 'Eliminar',
                    text: '¿Desea eliminar todos los elementos?',
                    type: 'warning',
                    //animation: "slide-from-top",
                    showCancelButton: true,
                    confirmButtonText: 'Eliminar',
                    closeOnConfirm: false,
                    showLoaderOnConfirm: true,
                }, function () {
                    $.post(action).done(function () {
                        swal.close();

                        cart.find('ul').html('');
                        cart.find('.list-count').text(0);
                        if (isNotificacion) {
                            Notificaciones.WriteCookie(0);
                        }
                    });
                });
            }
        });

        ////Notificacion mensaje
        //$('body').on('click', '.media.mail a', function () {
        //    var item = $(this).closest('.media');
        //    var IdEmisor = item.data('id-emisor');
        //    if (IdEmisor) {
        //        var Nombre = item.find('.media-heading').text();
        //        appSignalr.Chat.Methods.ConnectWithClient(IdEmisor, Nombre);
        //    }
        //});

        //Leemos las cookies
        Notificaciones.ReadCookie();
    }

    var handlePropagation = function () {
        $('body').on('click', '.dropdown-list .dropdown-menu', function (e) {
            e.stopPropagation();
        });
    }

    return {
        init: function () {
            handleBag();
            handlePropagation();            
        }
    };
}();

$(function () {    
    AppTheme.init();
    AppLayout.init();
});