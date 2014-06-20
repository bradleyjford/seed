(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('LookupListController', ['$scope', 'items', 'model', function ($scope, items, model) {
        $scope.model = model;

        $scope.pagedItems = items;

        $scope.page.title = 'Manage ' + model.typeName;
    }]);
})(angular);