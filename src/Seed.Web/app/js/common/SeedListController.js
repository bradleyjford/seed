(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('SeedListController', ['$state', '$stateParams', 'pagedResult', function ($state, $stateParams, pagedResult) {
        var self = this;

        function reload(page, pageSize, sortOrder, query) {
            $state.go('.', { pn: page, ps: pageSize, q: query, s: sortOrder.toString() });
        }

        this.pagedResult = pagedResult;
        this.query = $stateParams.q;
        this.sortParams = SortParams.parseString($stateParams.s || $state.current.data.defaultSort);

        this.pageChanged = function () {
            return reload(self.pagedResult.pageNumber, self.pagedResult.pageSize, self.sortParams, self.query);
        };

        this.sort = function (property) {
            if (self.sortParams.property === property) {
                self.sortParams.ascending = !self.sortParams.ascending;
            }
            else {
                self.sortParams.property = property;
                self.sortParams.ascending = true;
            }

            return reload(1, self.pagedResult.pageSize, self.sortParams, self.query);
        };

        this.search = function () {
            return reload(1, self.pagedResult.pageSize, self.sortParams, self.query);
        };
    }]);
})(angular);
