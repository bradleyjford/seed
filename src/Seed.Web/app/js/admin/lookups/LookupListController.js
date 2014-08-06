(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('LookupListController', ['$scope', '$state', 'items', 'model', function ($scope, $state, items, model) {
        $scope.model = model;

        $scope.pagedItems = items;

        $scope.page.title = 'Manage ' + model.typeName;

        $scope.add = function() {
            $state.go('app.admin.lookups.list.create');
        };
    }]);
})(angular);