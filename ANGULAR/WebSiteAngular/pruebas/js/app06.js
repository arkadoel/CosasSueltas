(function () {
    var app = angular.module('store', []);

    var miProducto ={
        nombre:'cosa',
        precio: 32.2
    }


    app.controller('StoreController', function () {
        this.product = miProducto;
    });
})();