(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserListController', ['users', '$state', '$stateParams', function (users, $state, $stateParams) {
        var self = this;

        this.pagedItems = users;

        this.filter = $stateParams.f;

        function getSortOrder(sortOrderString) {
            var parts = sortOrderString.split(' ');

            var result = {
                property: parts[0],
                ascending: true
            };

            if (parts[1] === 'desc') {
                result.ascending = false;
            }

            return result;
        }

        this.sortOrder = getSortOrder($stateParams.s || 'Id asc');

        function getSortOrderString (sortOrder) {
            var direction = sortOrder.ascending ? 'asc' : 'desc';

            return sortOrder.property + ' ' + direction;
        }

        this.reloadData = function (page, pageSize, sortOrder, filter) {
            $state.go('.', { p: page, c: pageSize, f: filter, s: getSortOrderString(sortOrder) });
        };

        this.pageChanged = function () {
            return self.reloadData(self.pagedItems.pageNumber, self.pagedItems.pageSize, self.sortOrder, self.filter);
        };

        this.orderBy = function (property) {
            if (self.sortOrder.property === property) {
                self.sortOrder.ascending = !self.sortOrder.ascending;
            }
            else {
                self.sortOrder.property = property;
                self.sortOrder.ascending = true;
            }

            return self.reloadData(1, self.pagedItems.pageSize, self.sortOrder, self.filter);
        };

        this.search = function () {
            return self.reloadData(1, self.pagedItems.pageSize, self.sortOrder, self.filter);
        };
    }]);
})(angular);
