'use strict';

const fs = require('fs');
const input = fs.readFileSync('input.txt', 'utf-8')

function part1(input) {
    const pairs = [];
    input.split('\r\n\r\n').forEach(pair => pairs.push([pair.split('\r\n')[0], pair.split('\r\n')[1]]) )

    let sum = 0;
    for (let i = 0; i < pairs.length; i++) if (compare(JSON.parse(pairs[i][0]), JSON.parse(pairs[i][1]))) sum += i + 1;

    return (sum);
}

function part2(input) {
    let splitInput = input
        .split('\r\n')
        .filter(element => element != '')
        .map(entry => JSON.parse(entry));
    splitInput.push([[2]], [[6]])

    splitInput.sort((a, b) => {
        const res = compare(a, b);
        if (res != null) return res ? -1 : 1;
        else return 0;
    })

    const first = splitInput.findIndex(element => JSON.stringify(element) === '[[2]]')
    const second = splitInput.findIndex(element => JSON.stringify(element) === '[[6]]')

    return (first + 1) * (second + 1);
}


function compare(a, b) {
    if (typeof a == 'number' && typeof b == 'number') {
        if (a > b) return false;
        if (a < b) return true;
        return null;
    } else if (Array.isArray(a) && Array.isArray(b)) {
        for (let i = 0; i < Math.min(a.length, b.length); i++) {
            let result = compare(a[i], b[i]);
            if (result != null) return result;
        }
        if (a.length < b.length) return true;
        else if (a.length > b.length) return false;
        return null;
    } else if (typeof a == 'number') return compare([a], b);
    else if (typeof b == 'number') return compare(a, [b]);
}

console.log(part1(input))
console.log(part2(input))
