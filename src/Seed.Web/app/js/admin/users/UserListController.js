(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserListController', ['$scope', 'users', 'UsersApi', function ($scope, users, UsersApi) {
        $scope.pagedItems = users;

        $scope.sortOrder = {
            property: 'Id',
            ascending: true
        };

        function getSortOrderString (sortOrder) {
            var direction = 'asc';

            if (!sortOrder.ascending) {
                direction = 'desc';
            }

            return sortOrder.property + ' ' + direction;
        }

        $scope.reloadData = function (page, pageSize, sortOrder) {
            var requestData = {
                pageNumber: page,
                pageSize: pageSize,
                sortOrder: getSortOrderString(sortOrder)
            };

            return UsersApi.query(requestData, function (data) {
                $scope.pagedItems = data;
            });
        };

        $scope.pageChanged = function() {
            return $scope.reloadData($scope.pagedItems.pageNumber, $scope.pagedItems.pageSize, $scope.sortOrder);
        };

        $scope.orderBy = function (property) {
            if ($scope.sortOrder.property === property) {
                $scope.sortOrder.ascending = !$scope.sortOrder.ascending;
            }
            else {
                $scope.sortOrder.property = property;
                $scope.sortOrder.ascending = true;
            }

            return $scope.reloadData(1, $scope.pagedItems.pageSize, $scope.sortOrder);
        };
    }]);
})(angular);