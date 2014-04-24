(function (angular){
    'use strict';

    var module = angular.module('seedApp.admin');

    module.factory('UsersApi', ['$resource', function ($resource) {
        return $resource('/api/admin/users/:userId');
    }]);
})(angular);