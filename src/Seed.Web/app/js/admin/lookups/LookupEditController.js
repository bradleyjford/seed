(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('LookupEditController', [
        '$scope', '$state', 'confirm', 'LookupsApi', 'model',

        function ($scope, $state, confirm, LookupsApi, model) {
            $scope.model = model;

            $scope.save = function (model) {
                if ($scope.editForm.$valid) {
                    model.$save({ id: model.id }, function () {
                        $scope.editForm.$setPristine();

                        $state.go('^', null, { reload: true });
                    });
                }
            };

            $scope.cancel = function() {
                $scope.editForm.$setPristine();

                $state.go('^');
            };

            $scope.enable = function (model) {
                confirm.show(
                    model,
                    'Enable {{ name }}?',
                    'Are you sure that you wish to enable the {{ typeName }} "{{ name }}"?',
                    {
                        okButtonText: 'Yes, enable',
                        cancelButtonText: 'No, cancel'
                    })
                    .then(function () {
                        LookupsApi.activate({ id: $scope.model.id }, null, function () {
                            $scope.model.isActive = true;
                        });
                    });
            };

            $scope.disable = function (model) {
                confirm.show(
                    model,
                    'Disable user "{{ name }}"?',
                    'Are you sure that you wish to disable {{ typeName }} "{{ name }}"?',
                    {
                        okButtonText: 'Yes, disable',
                        okButtonClass: 'btn-danger',
                        cancelButtonText: 'No, cancel'
                    })
                    .then(function () {
                        LookupsApi.deactivate({ id: $scope.model.id }, null, function () {
                            $scope.model.isActive = false;
                        });
                    });
            };
        }
    ]);
})(angular);
