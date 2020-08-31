/*! 
 * angular-hotkeys v1.5.0
 * https://chieffancypants.github.io/angular-hotkeys
 * Copyright (c) 2015 Wes Cruver
 * License: MIT
 */
!function () { "use strict"; angular.module("cfp.hotkeys", []).provider("hotkeys", ["$injector", function (a) { this.includeCheatSheet = !0, this.useNgRoute = a.has("ngViewDirective"), this.templateTitle = "Keyboard Shortcuts:", this.templateHeader = null, this.templateFooter = null, this.template = '<div class="cfp-hotkeys-container fade" ng-class="{in: helpVisible}" style="display: none;"><div class="cfp-hotkeys"><h4 class="cfp-hotkeys-title" ng-if="!header">{{ title }}</h4><div ng-bind-html="header" ng-if="header"></div><table><tbody><tr ng-repeat="hotkey in hotkeys | filter:{ description: \'!$$undefined$$\' }"><td class="cfp-hotkeys-keys"><span ng-repeat="key in hotkey.format() track by $index" class="cfp-hotkeys-key">{{ key }}</span></td><td class="cfp-hotkeys-text">{{ hotkey.description }}</td></tr></tbody></table><div ng-bind-html="footer" ng-if="footer"></div><div class="cfp-hotkeys-close" ng-click="toggleCheatSheet()">×</div></div></div>', this.cheatSheetHotkey = "?", this.cheatSheetDescription = "Show / hide this help menu", this.$get = ["$rootElement", "$rootScope", "$compile", "$window", "$document", function (a, b, c, d, e) { function f(a) { var b = { command: "⌘", shift: "⇧", left: "←", right: "→", up: "↑", down: "↓", "return": "↩", backspace: "⌫" }; a = a.split("+"); for (var c = 0; c < a.length; c++) "mod" === a[c] && (d.navigator && d.navigator.platform.indexOf("Mac") >= 0 ? a[c] = "command" : a[c] = "ctrl"), a[c] = b[a[c]] || a[c]; return a.join(" + ") } function g(a, b, c, d, e, f) { this.combo = a instanceof Array ? a : [a], this.description = b, this.callback = c, this.action = d, this.allowIn = e, this.persistent = f, this._formated = null } function h() { for (var a = o.hotkeys.length; a--;) { var b = o.hotkeys[a]; b && !b.persistent && k(b) } } function i() { o.helpVisible = !o.helpVisible, o.helpVisible ? (t = l("esc"), k("esc"), j("esc", t.description, i, null, ["INPUT", "SELECT", "TEXTAREA"])) : (k("esc"), t !== !1 && j(t)) } function j(a, b, c, d, e, f) { var h, i = ["INPUT", "SELECT", "TEXTAREA"], j = Object.prototype.toString.call(a); if ("[object Object]" === j && (b = a.description, c = a.callback, d = a.action, f = a.persistent, e = a.allowIn, a = a.combo), b instanceof Function ? (d = c, c = b, b = "$$undefined$$") : angular.isUndefined(b) && (b = "$$undefined$$"), void 0 === f && (f = !0), "function" == typeof c) { h = c, e instanceof Array || (e = []); for (var k, l = 0; l < e.length; l++) e[l] = e[l].toUpperCase(), k = i.indexOf(e[l]), -1 !== k && i.splice(k, 1); c = function (a) { var b = !0, c = a.target || a.srcElement, d = c.nodeName.toUpperCase(); if ((" " + c.className + " ").indexOf(" mousetrap ") > -1) b = !0; else for (var e = 0; e < i.length; e++) if (i[e] === d) { b = !1; break } b && n(h.apply(this, arguments)) } } "string" == typeof d ? Mousetrap.bind(a, n(c), d) : Mousetrap.bind(a, n(c)); var m = new g(a, b, c, d, e, f); return o.hotkeys.push(m), m } function k(a) { var b = a instanceof g ? a.combo : a; if (Mousetrap.unbind(b), angular.isArray(b)) { for (var c = !0, d = b.length; d--;) c = k(b[d]) && c; return c } var e = o.hotkeys.indexOf(l(b)); return e > -1 ? (o.hotkeys[e].combo.length > 1 ? o.hotkeys[e].combo.splice(o.hotkeys[e].combo.indexOf(b), 1) : o.hotkeys.splice(e, 1), !0) : !1 } function l(a) { if (!a) return o.hotkeys; for (var b, c = 0; c < o.hotkeys.length; c++) if (b = o.hotkeys[c], b.combo.indexOf(a) > -1) return b; return !1 } function m(a) { return a.$id in p || (p[a.$id] = [], a.$on("$destroy", function () { for (var b = p[a.$id].length; b--;) k(p[a.$id].pop()) })), { add: function (b) { var c; return c = arguments.length > 1 ? j.apply(this, arguments) : j(b), p[a.$id].push(c), this } } } function n(a) { return function (c, d) { if (a instanceof Array) { var e = a[0], f = a[1]; a = function (a) { f.scope.$eval(e) } } b.$apply(function () { a(c, l(d)) }) } } Mousetrap.prototype.stopCallback = function (a, b) { return (" " + b.className + " ").indexOf(" mousetrap ") > -1 ? !1 : b.contentEditable && "true" == b.contentEditable }, g.prototype.format = function () { if (null === this._formated) { for (var a = this.combo[0], b = a.split(/[\s]/), c = 0; c < b.length; c++) b[c] = f(b[c]); this._formated = b } return this._formated }; var o = b.$new(); o.hotkeys = [], o.helpVisible = !1, o.title = this.templateTitle, o.header = this.templateHeader, o.footer = this.templateFooter, o.toggleCheatSheet = i; var p = []; if (this.useNgRoute && b.$on("$routeChangeSuccess", function (a, b) { h(), b && b.hotkeys && angular.forEach(b.hotkeys, function (a) { var c = a[2]; ("string" == typeof c || c instanceof String) && (a[2] = [c, b]), a[5] = !1, j.apply(this, a) }) }), this.includeCheatSheet) { var q = e[0], r = a[0], s = angular.element(this.template); j(this.cheatSheetHotkey, this.cheatSheetDescription, i), (r === q || r === q.documentElement) && (r = q.body), angular.element(r).append(c(s)(o)) } var t = !1, u = { add: j, del: k, get: l, bindTo: m, template: this.template, toggleCheatSheet: i, includeCheatSheet: this.includeCheatSheet, cheatSheetHotkey: this.cheatSheetHotkey, cheatSheetDescription: this.cheatSheetDescription, useNgRoute: this.useNgRoute, purgeHotkeys: h, templateTitle: this.templateTitle }; return u }] }]).directive("hotkey", ["hotkeys", function (a) { return { restrict: "A", link: function (b, c, d) { var e, f; angular.forEach(b.$eval(d.hotkey), function (b, c) { f = "string" == typeof d.hotkeyAllowIn ? d.hotkeyAllowIn.split(/[\s,]+/) : [], e = c, a.add({ combo: c, description: d.hotkeyDescription, callback: b, action: d.hotkeyAction, allowIn: f }) }), c.bind("$destroy", function () { a.del(e) }) } } }]).run(["hotkeys", function (a) { }]) }(), function (a, b, c) { function d(a, b, c) { return a.addEventListener ? void a.addEventListener(b, c, !1) : void a.attachEvent("on" + b, c) } function e(a) { if ("keypress" == a.type) { var b = String.fromCharCode(a.which); return a.shiftKey || (b = b.toLowerCase()), b } return r[a.which] ? r[a.which] : s[a.which] ? s[a.which] : String.fromCharCode(a.which).toLowerCase() } function f(a, b) { return a.sort().join(",") === b.sort().join(",") } function g(a) { var b = []; return a.shiftKey && b.push("shift"), a.altKey && b.push("alt"), a.ctrlKey && b.push("ctrl"), a.metaKey && b.push("meta"), b } function h(a) { return a.preventDefault ? void a.preventDefault() : void (a.returnValue = !1) } function i(a) { return a.stopPropagation ? void a.stopPropagation() : void (a.cancelBubble = !0) } function j(a) { return "shift" == a || "ctrl" == a || "alt" == a || "meta" == a } function k() { if (!q) { q = {}; for (var a in r) a > 95 && 112 > a || r.hasOwnProperty(a) && (q[r[a]] = a) } return q } function l(a, b, c) { return c || (c = k()[a] ? "keydown" : "keypress"), "keypress" == c && b.length && (c = "keydown"), c } function m(a) { return "+" === a ? ["+"] : (a = a.replace(/\+{2}/g, "+plus"), a.split("+")) } function n(a, b) { var c, d, e, f = []; for (c = m(a), e = 0; e < c.length; ++e) d = c[e], u[d] && (d = u[d]), b && "keypress" != b && t[d] && (d = t[d], f.push("shift")), j(d) && f.push(d); return b = l(d, f, b), { key: d, modifiers: f, action: b } } function o(a, c) { return a === b ? !1 : a === c ? !0 : o(a.parentNode, c) } function p(a) { function c(a) { a = a || {}; var b, c = !1; for (b in u) a[b] ? c = !0 : u[b] = 0; c || (x = !1) } function k(a, b, c, d, e, g) { var h, i, k = [], l = c.type; if (!s._callbacks[a]) return []; for ("keyup" == l && j(a) && (b = [a]), h = 0; h < s._callbacks[a].length; ++h) if (i = s._callbacks[a][h], (d || !i.seq || u[i.seq] == i.level) && l == i.action && ("keypress" == l && !c.metaKey && !c.ctrlKey || f(b, i.modifiers))) { var m = !d && i.combo == e, n = d && i.seq == d && i.level == g; (m || n) && s._callbacks[a].splice(h, 1), k.push(i) } return k } function l(a, b, c, d) { s.stopCallback(b, b.target || b.srcElement, c, d) || a(b, c) === !1 && (h(b), i(b)) } function m(a) { "number" != typeof a.which && (a.which = a.keyCode); var b = e(a); if (b) return "keyup" == a.type && v === b ? void (v = !1) : void s.handleKey(b, g(a), a) } function o() { clearTimeout(t), t = setTimeout(c, 1e3) } function q(a, b, d, f) { function g(b) { return function () { x = b, ++u[a], o() } } function h(b) { l(d, b, a), "keyup" !== f && (v = e(b)), setTimeout(c, 10) } u[a] = 0; for (var i = 0; i < b.length; ++i) { var j = i + 1 === b.length, k = j ? h : g(f || n(b[i + 1]).action); r(b[i], k, f, a, i) } } function r(a, b, c, d, e) { s._directMap[a + ":" + c] = b, a = a.replace(/\s+/g, " "); var f, g = a.split(" "); return g.length > 1 ? void q(a, g, b, c) : (f = n(a, c), s._callbacks[f.key] = s._callbacks[f.key] || [], k(f.key, f.modifiers, { type: f.action }, d, a, e), void s._callbacks[f.key][d ? "unshift" : "push"]({ callback: b, modifiers: f.modifiers, action: f.action, seq: d, level: e, combo: a })) } var s = this; if (a = a || b, !(s instanceof p)) return new p(a); s.target = a, s._callbacks = {}, s._directMap = {}; var t, u = {}, v = !1, w = !1, x = !1; s._handleKey = function (a, b, d) { var e, f = k(a, b, d), g = {}, h = 0, i = !1; for (e = 0; e < f.length; ++e) f[e].seq && (h = Math.max(h, f[e].level)); for (e = 0; e < f.length; ++e) if (f[e].seq) { if (f[e].level != h) continue; i = !0, g[f[e].seq] = 1, l(f[e].callback, d, f[e].combo, f[e].seq) } else i || l(f[e].callback, d, f[e].combo); var m = "keypress" == d.type && w; d.type != x || j(a) || m || c(g), w = i && "keydown" == d.type }, s._bindMultiple = function (a, b, c) { for (var d = 0; d < a.length; ++d) r(a[d], b, c) }, d(a, "keypress", m), d(a, "keydown", m), d(a, "keyup", m) } for (var q, r = { 8: "backspace", 9: "tab", 13: "enter", 16: "shift", 17: "ctrl", 18: "alt", 20: "capslock", 27: "esc", 32: "space", 33: "pageup", 34: "pagedown", 35: "end", 36: "home", 37: "left", 38: "up", 39: "right", 40: "down", 45: "ins", 46: "del", 91: "meta", 93: "meta", 224: "meta" }, s = { 106: "*", 107: "+", 109: "-", 110: ".", 111: "/", 186: ";", 187: "=", 188: ",", 189: "-", 190: ".", 191: "/", 192: "`", 219: "[", 220: "\\", 221: "]", 222: "'" }, t = { "~": "`", "!": "1", "@": "2", "#": "3", $: "4", "%": "5", "^": "6", "&": "7", "*": "8", "(": "9", ")": "0", _: "-", "+": "=", ":": ";", '"': "'", "<": ",", ">": ".", "?": "/", "|": "\\" }, u = { option: "alt", command: "meta", "return": "enter", escape: "esc", plus: "+", mod: /Mac|iPod|iPhone|iPad/.test(navigator.platform) ? "meta" : "ctrl" }, v = 1; 20 > v; ++v) r[111 + v] = "f" + v; for (v = 0; 9 >= v; ++v) r[v + 96] = v; p.prototype.bind = function (a, b, c) { var d = this; return a = a instanceof Array ? a : [a], d._bindMultiple.call(d, a, b, c), d }, p.prototype.unbind = function (a, b) { var c = this; return c.bind.call(c, a, function () { }, b) }, p.prototype.trigger = function (a, b) { var c = this; return c._directMap[a + ":" + b] && c._directMap[a + ":" + b]({}, a), c }, p.prototype.reset = function () { var a = this; return a._callbacks = {}, a._directMap = {}, a }, p.prototype.stopCallback = function (a, b) { var c = this; return (" " + b.className + " ").indexOf(" mousetrap ") > -1 ? !1 : o(b, c.target) ? !1 : "INPUT" == b.tagName || "SELECT" == b.tagName || "TEXTAREA" == b.tagName || b.isContentEditable }, p.prototype.handleKey = function () { var a = this; return a._handleKey.apply(a, arguments) }, p.init = function () { var a = p(b); for (var c in a) "_" !== c.charAt(0) && (p[c] = function (b) { return function () { return a[b].apply(a, arguments) } }(c)) }, p.init(), a.Mousetrap = p, "undefined" != typeof module && module.exports && (module.exports = p), "function" == typeof define && define.amd && define(function () { return p }) }(window, document);