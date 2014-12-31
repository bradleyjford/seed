(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('LookupCreateController', [
        '$scope', '$state', 'confirm', 'LookupsApi', 'model',

        function ($scope, $state, confirm, LookupsApi) {
            $scope.model = new LookupsApi();

            $scope.model.name = '';

            $scope.save = function (model) {
                if ($scope.createForm.$valid) {
                    model.$save({ id: model.id, type: 'countries' }, function () {
                        $scope.createForm.$setPristine();

                        $state.go('^', null, { reload: true });
                    });
                }
            };

            $scope.cancel = function() {
                $scope.createForm.$setPristine();

                $state.go('^');
            };
        }
    ]);
})(angular);
