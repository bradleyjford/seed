(function (angular){
    'use strict';

    var module = angular.module('seedApp.admin');

    module.factory('LookupsApi', ['$resource', function ($resource) {
        return $resource('/api/admin/lookups/:type/:id', null, {
            query: { isArray: false },
            activate: { method: 'POST', url: '/api/admin/lookups/:type/:id/activate' },
            deactivate: { method: 'POST', url: '/api/admin/lookups/:type/:id/deactivate' }
        });
    }]);
})(angular);