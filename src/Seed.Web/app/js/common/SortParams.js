SortParams = (function () {
    'use strict';

    function SortParams() {

    }

    SortParams.prototype.property = '';
    SortParams.prototype.ascending = true;

    SortParams.prototype.toString = function () {
        var direction = this.ascending ? 'asc' : 'desc';

        return this.property + ' ' + direction;
    };

    SortParams.parseString = function (value) {
        var parts = value.split(' ');

        var result = new SortParams();

        result.property = parts[0];
        result.ascending = true;

        if (parts[1] === 'desc') {
            result.ascending = false;
        }

        return result;
    };

    return SortParams;
})();
