(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.factory('UsersApi', ['$resource', function ($resource) {
        return $resource('/api/admin/users/:userId', null, {
            query: { isArray: false },
            activate: { method: 'POST', url: '/api/admin/users/:userId/activate' },
            deactivate: { method: 'POST', url: '/api/admin/users/:userId/deactivate' }
        });
    }]);
})(angular);
