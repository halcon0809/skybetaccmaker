let lastBedValue;


function getFiber(dom) {
    console.log('getFiber()');
    const key = Object.keys(dom).find(key => key.startsWith("__reactFiber$"));
    if (!key)
        return null;
    return dom[key];
}

function getNavigator() {
    console.log('getNavigator()');
    return getFiber(document.querySelector('main')).return.return.return.return.return.return.pendingProps.value;
}


function clickToMatch(url) {
    console.log('clickToMatch()');
    var urlObj = new URL(url);
    if (urlObj.pathname == location.pathname)
        return true;

    var pushObject = { pathname: urlObj.pathname, hash: "", search: "" };

    getNavigator().push(pushObject);
    return true;
}

function waitLoadPageMatch(eventId) {
    console.log('waitLoadPageMatch()');
    return getNavigator().location.pathname.split('/').pop() == eventId;
}

//getFiber(document.querySelector('main')).return.return.return.return.return.return.pendingProps.value.push

function convertNumber(input) {
    console.log('convertNumber()');
    return parseFloat(input.toString().replace(',', '.').replace(/\s/g, ''));
}

//Ввод суммы в купон
function insertBedValue(value) {
    console.log('insertBedValue()');
    var input = document.querySelector('input[data-atid="miniTicket-amountInput"]');

    input.click();

    function sendChange(target, val) {
        var lastValue = target.value;
        target.value = val;
        var event = new Event('input', { bubbles: true });
        event.simulated = true;
        target._valueTracker && target._valueTracker.setValue(lastValue);
        target.dispatchEvent(event);
    }
    function sendBlur(target) {
        target.dispatchEvent(new FocusEvent('blur', { 'bubbles': true, 'cancelable': false }));
    }
    sendChange(input, value)
    sendBlur(input)
    return true;
}

//Сумма с купона
function getBedValue() {
    console.log('getBedValue()');
    const inputValue = document.querySelector('input[data-atid="miniTicket-amountInput"]');
    return inputValue.value.replace(/\s+/, "");
}

//Мин ставка
function getLimitMatchMin() {
    console.log('getLimitMatchMin()');
    return document.querySelector('button[data-atid="depositBtn-0"]').innerText
}

//Макс ставка
function getLimitMatchMax() {
    console.log('getLimitMatchMax()');
    return document.querySelector('button[data-atid="depositBtn-3"]').innerText
}


function getBedValueCoupon(insertLastBed = true) {
    console.log('getBedValueCoupon()');
    const betValue = document.querySelector('span[data-atid="miniticket-rate"]');

    let returnValue;

    if (betValue && betValue.innerText != "" && betValue.innerText != '--')
        returnValue = betValue.innerText;
    else returnValue = null;

    if (insertLastBed)
        lastBedValue = convertNumber(returnValue);

    return returnValue;
}

function sendBed(value) {
    console.log('sendBed()');
    let cureBedValue = convertNumber(getBedValueCoupon(false));
    if (cureBedValue < lastBedValue - 0.03)
        return false;

    const buttonSend = document.querySelector('button[data-atid="miniticketBetBtn"]');

    if (buttonSend.hasAttribute('disabled'))
        return 'disabled';

    if (!buttonSend)
        return false;

    //document.querySelector('.m-ticketMessage__btn')?.click();
    //document.querySelector('input[data-atid="miniTicket-amountInput"]')?.click();

    const valueInput = convertNumber(getBedValue());
    const value2 = convertNumber(value);

    if (valueInput == value2) {
        //betslip.makeBet();
        //getTicket().betting = true;

        if (buttonSend.hasAttribute('disabled'))
            return 'disabled';

        buttonSend.click();
        return true;
    }
    return false;
}


function getTicket() {
    console.log('getTicket()');
    var ticketBuilderSelector = document.querySelector('div[data-atid="miniticket"]');

    const ticketBettingBuilder = getFiber(ticketBuilderSelector);
    return ticketBettingBuilder.return.return.return.pendingProps;

}




function getResult() {
    console.log('getResult()');
    if (document.querySelector('.sc-ecdDTd.fZCDAS')) {
        return true;
    }
    if (document.querySelector('[data-atid="modalDialog-content"]')) {
        if (document.querySelector('[data-atid="modalDialog-content"]').innerText == 'Na tiketu se změnily kurzy. Tiket nebyl vsazen.')
            return "change koef";
        if (document.querySelector('[data-atid="modalDialog-content"]').innerText == 'Někde se stala chyba. Zkontrolujte tiket v sekci Moje tikety.')
            return "redTable";
        return false;
    }
    if (document.querySelector('.sc-fsQiph.eNdWjB'))
        return "reload";
    return "nope";
}


//+
function removeCoupon() {
    console.log('removeCoupon()');
    const closeButton = document.querySelector('[data-atid="miniTicket-clearTicket"]');
    if (closeButton)
        closeButton.click()

    return true;
}

function checkEmptyCoupon() {
    console.log('checkEmptyCoupon()');
    if (document.querySelector('.sc-ZzDLD.bKLMkM'))
        return true;
    else
        return false
}

function SendRequest(oppId) {
    var url = "https://m.tipsport.cz/rest/ticket-builder/v1/ticket-builders/1/opportunities?responseChannel=SOCKET_ONLY";
    const data = {
        opportunityIds: [oppId],
        "fromBetSuggestion": false,
	    "ticketOrigin":"TOP_TEN",
        "opportunitiesOrigin": "STANDARD_OFFER"
    };
    var json = JSON.stringify(data);
    var xhr = new XMLHttpRequest();
    xhr.open("PUT", url, true);
    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhr.onload = function () { }
        ;
    xhr.send(json);
}


function clickBedEventId(rgroupName, rgroupRowName, rstakeName) {
    console.log('clickBedEventId()');

    if (document.querySelector('p[data-atid="emptyCard-message"]')) {
        return "reload";
    }

    console.log(rgroupName);
    console.log(rgroupRowName);
    console.log(rstakeName);
    console.log('clickBedEventId(' + rgroupName + ',' + rgroupRowName + ',' + rstakeName + ')');
    var groupName = rgroupName;
    var stakeName = rstakeName;
    var groupRowName = rgroupRowName;
    var listblock = document.querySelectorAll('.sc-UxxwN.dsLGbC')
    let isSuccess = false;
    listblock.forEach(element => {
        if (isSuccess)
            return true;
        if (!element.children[0].children[0])
            return false;
        var groupNameBK = element.children[0].children[0].innerText

        if (groupNameBK) {
            if (groupName == groupNameBK) {
                if (groupRowName != '') {
                    var listrow = element.querySelectorAll('.sc-UxxwN.dsLGbC')
                    if (listrow.length != 0) {
                        let nameRowGroup = ''
                        for (let i = 0; i < listrow.length; i++) {
                            if (listrow[i].querySelector('.sc-bbpGSz.jsWRxl'))
                                nameRowGroup = listrow[i].querySelector('.sc-bbpGSz.jsWRxl').innerText;
                            //
                            if (nameRowGroup == groupRowName) {
                                if (listrow[i].querySelector('.sc-kGTyPW.bKPOwp')) {
                                    var listBets = listrow[i].querySelectorAll('.sc-kGTyPW.bKPOwp')
                                    listBets.forEach(bet => {
                                        //console.log(bet.innerText)
                                        if (bet.innerText == stakeName) {
                                            bet.click();
                                            return isSuccess = true;
                                        }
                                    })
                                }
                                if (listrow[i].querySelector('.sc-kGTyPW.gBpjyZ')) {
                                    var listBets = listrow[i].querySelectorAll('.sc-kGTyPW.gBpjyZ')
                                    listBets.forEach(bet => {
                                        //console.log(bet.innerText)
                                        if (bet.innerText == stakeName) {
                                            bet.click();
                                            return isSuccess = true;
                                        }
                                    })
                                }
                            }
                        }
                    }
                    var listrow2 = element.querySelectorAll('.sc-UxxwN.dsLGbC');
                    if (listrow2.length != 0) {
                        listrow2.forEach(row => {
                            if (row.querySelector('.sc-jPYHJC.cLPnkd')) {
                                if (row.querySelector('.sc-jPYHJC.cLPnkd').innerText == groupRowName) {
                                    var listBets = row.querySelectorAll('.sc-bSkxYT.icKJEx')
                                    listBets.forEach(bet => {
                                        //console.log(bet.querySelector('.sc-kGTyPW.bKPOwp').innerText)
                                        if (bet.querySelector('.sc-kGTyPW.bKPOwp')) {
                                            if (bet.querySelector('.sc-kGTyPW.bKPOwp').innerText == stakeName) {
                                                bet.click();
                                                return isSuccess = true;
                                            }
                                        }
                                        if (bet.querySelector('.sc-kGTyPW.gBpjyZ')) {
                                            if (bet.querySelector('.sc-kGTyPW.gBpjyZ').innerText == stakeName) {
                                                bet.click();
                                                return isSuccess = true;
                                            }
                                        }
                                    })
                                }
                            }
                        })
                    }

                }
                else {
                    var listBets = element.querySelectorAll('.sc-bSkxYT.icKJEx')
                    listBets.forEach(bet => {
                        //console.log(bet.querySelector('.sc-kGTyPW.bKPOwp').innerText)
                        if (bet.querySelector('.sc-kGTyPW.bKPOwp')) {
                            if (bet.querySelector('.sc-kGTyPW.bKPOwp').innerText == stakeName) {
                                bet.click();
                                return isSuccess = true;
                            }
                        }
                        if (bet.querySelector('.sc-kGTyPW.gBpjyZ')) {
                            if (bet.querySelector('.sc-kGTyPW.gBpjyZ').innerText == stakeName) {
                                bet.click();
                                return isSuccess = true;
                            }
                        }
                    })
                }
            }
        }
        else {
            return "new group";
        }
    });
    return isSuccess;
}

function getBedValueTextEventId(rgroupName, rgroupRowName, rstakeName) {
    console.log('getBedValueTextEventId()');
    if (document.querySelector('p[data-atid="emptyCard-message"]')) {
        return "reload";
    }

    console.log(rgroupName);
    console.log(rgroupRowName);
    console.log(rstakeName);
    var groupName = rgroupName;
    var stakeName = rstakeName;
    var groupRowName = rgroupRowName;
    var listblock = document.querySelectorAll('.sc-UxxwN.dsLGbC')
    let koef = 0;
    listblock.forEach(element => {

        if (koef != 0)
            return koef;
        if (!element.children[0].children[0])
            return 0;
        var groupNameBK = element.children[0].children[0].innerText

        console.log(groupNameBK);
        //var groupNameBK
        //if (element.querySelector('.sc-gmkxkN.kyIjqG'))
        //    groupNameBK = element.querySelector('.sc-gmkxkN.kyIjqG').innerText
        //if (element.querySelector('.sc-djpCmO.bAmSMB'))
        //    groupNameBK = element.querySelector('.sc-djpCmO.bAmSMB').innerText
        //if (element.querySelector('.sc-gewWuj.dHeYbD'))
        //    groupNameBK = element.querySelector('.sc-gewWuj.dHeYbD').innerText // sc - gewWuj dHeYbD
        //if (element.querySelector('.sc-gWWqcY.zMgWa'))
        //    groupNameBK = element.querySelector('.sc-gWWqcY.zMgWa').innerText //sc-djpCmO IXGlF
        //if (element.querySelector('.sc-djpCmO.IXGlF'))
        //    groupNameBK = element.querySelector('.sc-djpCmO.IXGlF').innerText  //sc-gmkxkN hFvEei //sc-gewWuj NSsPf // //sc-gmkxkN hFvEei //sc-cXghZX BmUfU
        //if (element.querySelector('.sc-gmkxkN.hFvEei'))
        //    groupNameBK = element.querySelector('.sc-gmkxkN.hFvEei').innerText
        //if (element.querySelector('.sc-gewWuj.NSsPf'))
        //    groupNameBK = element.querySelector('.sc-gewWuj.NSsPf').innerText // sc-gewWuj dHeYbD
        //if (element.querySelector('.sc-cXghZX.BmUfU'))
        //    groupNameBK = element.querySelector('.sc-cXghZX.BmUfU').innerText//sc-eRpgxp hIajpq
        if (groupNameBK) { 
            if (groupName == groupNameBK) {
                if (groupRowName != '') {
                    var listrow = element.querySelectorAll('.sc-UxxwN.dsLGbC')
                    //console.log(listrow.length != 0);
                    if (listrow.length != 0) {
                        let nameRowGroup = ''
                        for (let i = 0; i < listrow.length; i++) {
                            if (listrow[i].querySelector('.sc-bbpGSz.jsWRxl'))
                                nameRowGroup = listrow[i].querySelector('.sc-bbpGSz.jsWRxl').innerText;
                            //
                            if (nameRowGroup == groupRowName) {
                                var listBets = listrow[i].querySelectorAll('.sc-bSkxYT.icKJEx')
                                listBets.forEach(bet => {
                                    //console.log(bet.innerText)
                                    if (bet.querySelector('.sc-kGTyPW.bKPOwp')) {
                                        if (bet.querySelector('.sc-kGTyPW.bKPOwp').innerText == stakeName) {
                                            return koef = bet.querySelector('.sc-hNeXkk.cDgPaY').innerText
                                        }
                                    }
                                    if (bet.querySelector('.sc-kGTyPW.gBpjyZ')) {
                                        if (bet.querySelector('.sc-kGTyPW.gBpjyZ').innerText == stakeName) {
                                            return koef = bet.querySelector('.sc-hNeXkk.bjobDO').innerText
                                        }
                                    }
                                })
                            }
                        }
                    }
                    var listrow2 = element.querySelectorAll('.sc-UxxwN.dsLGbC');
                    if (listrow2.length != 0) {
                        listrow2.forEach(row => {
                            if (row.querySelector('.sc-jPYHJC.cLPnkd')) {
                                if (row.querySelector('.sc-jPYHJC.cLPnkd').innerText == groupRowName) {
                                    var listBets = row.querySelectorAll('.sc-bSkxYT.icKJEx')
                                    listBets.forEach(bet => {
                                        //console.log(bet.querySelector('.sc-kGTyPW.bKPOwp').innerText)
                                        if (bet.querySelector('.sc-kGTyPW.bKPOwp')) {
                                            if (bet.querySelector('.sc-kGTyPW.bKPOwp').innerText == stakeName) {
                                                return koef = bet.querySelector('.sc-hNeXkk.cDgPaY').innerText
                                            }
                                        }
                                        if (bet.querySelector('.sc-kGTyPW.gBpjyZ')) {
                                            if (bet.querySelector('.sc-kGTyPW.gBpjyZ').innerText == stakeName) {
                                                return koef = bet.querySelector('.sc-hNeXkk.bjobDO').innerText
                                            }
                                        }
                                    })
                                }
                            }
                        })
                    }

                }
                else {
                    var listBets = element.querySelectorAll('.sc-bSkxYT.icKJEx')
                    listBets.forEach(bet => {
                        //console.log(bet.querySelector('.sc-kGTyPW.bKPOwp').innerText)
                        if (bet.querySelector('.sc-kGTyPW.bKPOwp')) {
                            if (bet.querySelector('.sc-kGTyPW.bKPOwp').innerText == stakeName) {
                                return koef = bet.querySelector('.sc-hNeXkk.cDgPaY').innerText
                            }
                        }
                        if (bet.querySelector('.sc-kGTyPW.gBpjyZ')) {
                            if (bet.querySelector('.sc-kGTyPW.gBpjyZ').innerText == stakeName) {
                                return koef = bet.querySelector('.sc-hNeXkk.bjobDO').innerText
                            }
                        }
                    })
                }
            }
        }
    });
    return koef;
}



function Login(login, password) {
    console.log('Login()');
    var loginI = document.querySelector('[data-atid="login-email"]');
    var passwordI = document.querySelector('[data-atid="login-password"]');

    function sendChange(target, val) {
        var lastValue = target.value;
        target.value = val;
        var event = new Event('input', { bubbles: true });
        event.simulated = true;
        target._valueTracker && target._valueTracker.setValue(lastValue);
        target.dispatchEvent(event);
    }
    function sendBlur(target) {
        target.dispatchEvent(new FocusEvent('blur', { 'bubbles': true, 'cancelable': false }));
    }

    sendChange(loginI, login)
    sendBlur(loginI)

    sendChange(passwordI, password)
    sendBlur(passwordI)

    document.querySelector('[data-atid="login-submit"]').click();

}

function checkLogin() {
    console.log('checkLogin()');
    return document.querySelector('[data-atid="header-account-btn"]') != null;
}



function clickToLoginPage(login, password) {
    console.log('clickToLoginPage()');
    if (location.pathname != '/prihlaseni') {
        clickToMatch('https://m.tipsport.cz/prihlaseni');
        return false;
    }
    Login(login, password);
    return true;
}


function getLocationHost() {
    console.log('getLocationHost()');
    return location.host
}


function getCurrentBalance() {
    console.log('getCurrentBalance()');
    //user-box-balance
    var balanceContainer = document.querySelector('[data-atid="header-account-btn"]');
    if (!balanceContainer)
        return -1;
    var balanceText = document.querySelector('[data-atid="header-account-btn"]').querySelector('.sc-kfzCjt').innerText;

    if (balanceText.includes(' '))
        balanceText = balanceText.slice(0, balanceText.lastIndexOf(' ')).replaceAll(' ', '').replace('Kč' , '').trim();
    else if (balanceText.includes(' '))
        balanceText = balanceText.slice(0, balanceText.lastIndexOf(' ')).replaceAll(' ', '').replace('Kč' , '').trim();

    return balanceText;
}

function clickSport(sportId) {
    if (sportId == '1') {
        if (document.querySelector('[data-atid="superSport-Fotbal"]')) {
            document.querySelector('[data-atid="superSport-Fotbal"]').click()
            return true;
        }
        else
            return false;
    }
    if (sportId == '2') {
        if (document.querySelector('[data-atid="superSport-Lední hokej"]')) {
            document.querySelector('[data-atid="superSport-Lední hokej"]').click()
            return true;
        }
        else
            return false;
    }
    if (sportId == '3') {
        if (document.querySelector('[data-atid="superSport-Tenis"]')) {
            document.querySelector('[data-atid="superSport-Tenis"]').click()
            return true;
        }
        else
            return false;
    }
    if (sportId == '4') {
        if (document.querySelector('[data-atid="superSport-Basketbal"]')) {
            document.querySelector('[data-atid="superSport-Basketbal"]').click()
            return true;
        }
        else
            return false;
    }
    return false;
}

function clickLive() {
    if (document.querySelector('[data-atid="nav.live"]')) {
        document.querySelector('[data-atid="nav.live"]').click()
        return true;
    }
    else
        return false;
}


function clickEvent(eventId) {
    var status = false
    if (document.querySelector('[data-atid="matchWrapper"]')) {
        var listEvent = document.querySelectorAll('[data-atid="matchWrapper"]')
        listEvent.forEach(element => {
            if (getFiber(element).child.pendingProps.matchData.id == eventId) {
                element.click();
                status = true;
                return;
            }
        });
        return status;
    }
}