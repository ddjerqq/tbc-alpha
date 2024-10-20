<script lang="ts">
  import * as Tabs from "$lib/components/ui/tabs/index.js";
  import * as Card from "$lib/components/ui/card/index.js";
  import {Button} from "$lib/components/ui/button/index.js";
  import {Input} from "$lib/components/ui/input/index.js";
  import {Label} from "$lib/components/ui/label/index.js";

  import * as AlertDialog from "$lib/components/ui/alert-dialog/index.js";

  import * as Form from "$lib/components/ui/form/index";

  import {Badge} from "$lib/components/ui/badge";
  import CreditCard from "$lib/components/CreditCard.svelte";

  import Icon from "heroicons-for-svelte";
  import {
    CreditCard as CreditCardIcon,
    Collection,
    ShoppingCart,
    EmojiHappy,
    SwitchHorizontal,
    DeviceMobile,
    Document,
    ReceiptTax,
    CurrencyDollar,
    Plus,
    PaperClip,
    Eye
  } from "heroicons-for-svelte/icons/outline";


  let advices = [
    "You've spent $1000 in your needs, which is 35% of your income and 30% of your total monthly spending. This means you are spending more than 70% of your total spending is in categories that are not very important.",
    "Your spending is too high at 89% of your income. It's time to reevaluate and cut down on non-essential costs.",
    "You're spending a significant amount 54% on shopping. Try to set a monthly budget to manage this better.",
    "You have $2000 saved so far in your emergency fund. To complete it, aim to save an additional $1500.",
    "You're saving 10% of your income. That's a good start, but you might want to increase your savings to prepare for long-term goals.",
    "You're making great progress! You've reached 64% of your goals. Keep going, you're almost there.",
  ];
</script>

<!-- qr login-->
<Card.Root class="rounded-2xl">
    <Card.Header>
        <Card.Title class="flex justify-between">
            <div class="flex items-center gap-2">
                <Icon icon={CreditCardIcon} class="text-2xl"/>
                Accounts
            </div>

            <div class="flex items-center text-gray-500 gap-2 text-sm">
                <AlertDialog.Root>
                    <AlertDialog.Trigger asChild let:builder>
                        <Button builders={[builder]} variant="outline" class="gap-2">
                            <Icon icon={Eye}/>
                            View card
                        </Button>
                    </AlertDialog.Trigger>
                    <AlertDialog.Content class="rounded-2xl">
                        <AlertDialog.Header>
                            <AlertDialog.Title class="text-start">MasterCard digital</AlertDialog.Title>
                            <AlertDialog.Description class="flex flex-col items-start font-mono">
                                <span>num: 4242 4242 4242 4242</span>
                                <span>exp: 4/42</span>
                                <span>cvc: 420</span>
                            </AlertDialog.Description>
                        </AlertDialog.Header>
                        <AlertDialog.Footer>
                            <AlertDialog.Action>Close</AlertDialog.Action>
                        </AlertDialog.Footer>
                    </AlertDialog.Content>
                </AlertDialog.Root>
            </div>
        </Card.Title>
    </Card.Header>

    <Card.Content>
        <CreditCard cardNumber="**** **** **** 4242" balance={4242.42}/>
    </Card.Content>

    <Card.Footer class="flex justify-between font-semibold">
                <span>
                    Total available:
                </span>
        <Badge variant="outline">
            $4242.42
        </Badge>
    </Card.Footer>
</Card.Root>

<!-- modular ui-->
<Card.Root class="rounded-2xl">
    <Card.Content class="w-full grid justify-between grid-cols-4 gap-3">
        <div class="bg-sky-50 text-sky-400 border-2 border-sky-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">
            <Icon icon={DeviceMobile} class="text-3xl"/>
            Top up
        </div>

        <a href="/transactions"
           class="bg-purple-50 text-purple-400 border-2 border-purple-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">
            <Icon icon={Collection} class="text-3xl"/>
            Txn
        </a>

        <div class="bg-green-50 text-green-400 border-2 border-green-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">
            <Icon icon={CurrencyDollar} class="text-3xl"/>
            Exchange
        </div>

        <div class="bg-yellow-50 text-yellow-400 border-2 border-yellow-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">
            <Icon icon={EmojiHappy} class="text-3xl"/>
            Shaurma
        </div>

        <!--                <AlertDialog.Root>-->
        <!--                    <AlertDialog.Trigger asChild let:builder>-->
        <!--                        <Button builders={[builder]}-->
        <!--                                class="hover:bg-gray-100 h-full bg-gray-50 text-gray-400 border-2 border-gray-200 border-dashed flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                            <Icon icon={Plus} class="text-3xl"/>-->
        <!--                        </Button>-->
        <!--                    </AlertDialog.Trigger>-->
        <!--                    <AlertDialog.Content class="rounded-2xl">-->
        <!--                        <AlertDialog.Header>-->
        <!--                            <AlertDialog.Title class="text-start">Add widget</AlertDialog.Title>-->
        <!--                            <AlertDialog.Description class="w-full grid grid-cols-3 gap-5">-->
        <!--                                <div class="bg-sky-50 text-sky-400 border-2 border-sky-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                                    <Icon icon={DeviceMobile} class="text-xl"/>-->
        <!--                                    Top up-->
        <!--                                </div>-->

        <!--                                <div class="bg-green-50 text-green-400 border-2 border-green-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                                    <Icon icon={SwitchHorizontal} class="text-xl"/>-->
        <!--                                    Transfer-->
        <!--                                </div>-->

        <!--                                <div class="bg-green-50 text-green-400 border-2 border-green-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                                    <Icon icon={CurrencyDollar} class="text-xl"/>-->
        <!--                                    Exchange-->
        <!--                                </div>-->

        <!--                                <div class="bg-blue-50 text-blue-400 border-2 border-blue-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                                    <Icon icon={Document} class="text-xl"/>-->
        <!--                                    Bill-->
        <!--                                </div>-->

        <!--                                <div class="bg-orange-50 text-orange-400 border-2 border-orange-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                                    <Icon icon={PaperClip} class="text-xl"/>-->
        <!--                                    Fines-->
        <!--                                </div>-->

        <!--                                <div class="bg-purple-50 text-purple-400 border-2 border-purple-100 flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                                    <Icon icon={ReceiptTax} class="text-xl"/>-->
        <!--                                    Offers-->
        <!--                                </div>-->
        <!--                            </AlertDialog.Description>-->
        <!--                        </AlertDialog.Header>-->
        <!--                        <AlertDialog.Footer>-->
        <!--                            <AlertDialog.Action>Close</AlertDialog.Action>-->
        <!--                        </AlertDialog.Footer>-->
        <!--                    </AlertDialog.Content>-->
        <!--                </AlertDialog.Root>-->

        <!--                <div class="bg-gray-50 text-gray-400 border-2 border-gray-200 border-dashed flex-1 aspect-square flex flex-col justify-center font-semibold text-sm items-center rounded-2xl">-->
        <!--                    <Icon icon={Plus} class="text-3xl"/>-->
        <!--                </div>-->
    </Card.Content>
</Card.Root>


<!-- transactions-->
<Card.Root class="rounded-2xl">
    <Card.Header>
        <Card.Title class="flex justify-between">
            <div class="flex items-center gap-2">
                <Icon icon={Collection} class="text-2xl"/>
                Transactions
            </div>

            <div class="flex items-center text-gray-500 gap-2 text-sm">
                <AlertDialog.Root>
                    <AlertDialog.Trigger asChild let:builder>
                        <Button builders={[builder]} variant="outline"
                                class="flex items-center gap-2 bg-green-50 border border-green-100 text-green-600">
                            <Icon icon={Eye} class="text-xl"/>
                            Get financial advice
                        </Button>
                    </AlertDialog.Trigger>
                    <AlertDialog.Content class="rounded-2xl">
                        <AlertDialog.Header>
                            <AlertDialog.Title class="text-start">Your financial advice</AlertDialog.Title>
                            <AlertDialog.Description class="flex flex-col items-stretch gap-3">
                                {#each advices as advice, i}
                                            <span class="rounded-xl p-3 text-sm border bg-green-50 border-green-300 text-green-600">
                                                {i + 1}. {advice}
                                            </span>
                                {/each}
                            </AlertDialog.Description>
                        </AlertDialog.Header>
                        <AlertDialog.Footer>
                            <AlertDialog.Action>Close</AlertDialog.Action>
                        </AlertDialog.Footer>
                    </AlertDialog.Content>
                </AlertDialog.Root>
            </div>
        </Card.Title>
    </Card.Header>

    <Card.Content class="w-full flex flex-col justify-start gap-3">
        <div class="flex justify-between">
            <div class="flex items-center gap-3">
                <div class="size-10 bg-gray-50 text-gray-300 border-2 border-gray-100 aspect-square flex justify-center items-center rounded-xl">
                    <Icon icon={ShoppingCart} class="text-xl"/>
                </div>
                <div class="flex flex-col -space-y-1.5">
                    <span class="font-semibold text-gray-700">Acme</span>
                    <span class="text-xs text-gray-400">Supplies</span>
                </div>
            </div>

            <div class="flex items-center font-semibold">
                -$42.42
            </div>
        </div>

        <div class="flex justify-between">
            <div class="flex items-center gap-3">
                <div class="size-10 bg-gray-50 text-gray-300 border-2 border-gray-100 aspect-square flex justify-center items-center rounded-xl">
                    <Icon icon={ShoppingCart} class="text-xl"/>
                </div>
                <div class="flex flex-col -space-y-1.5">
                    <span class="font-semibold text-gray-700">Acme</span>
                    <span class="text-xs text-gray-400">Supplies</span>
                </div>
            </div>

            <div class="flex items-center font-semibold">
                -$42.42
            </div>
        </div>

        <div class="flex justify-between">
            <div class="flex items-center gap-3">
                <div class="size-10 bg-gray-50 text-gray-300 border-2 border-gray-100 aspect-square flex justify-center items-center rounded-xl">
                    <Icon icon={ShoppingCart} class="text-xl"/>
                </div>
                <div class="flex flex-col -space-y-1.5">
                    <span class="font-semibold text-gray-700">Acme</span>
                    <span class="text-xs text-gray-400">Supplies</span>
                </div>
            </div>

            <div class="flex items-center font-semibold">
                -$42.42
            </div>
        </div>

        <div class="flex justify-between">
            <div class="flex items-center gap-3">
                <div class="size-10 bg-gray-50 text-gray-300 border-2 border-gray-100 aspect-square flex justify-center items-center rounded-xl">
                    <Icon icon={ShoppingCart} class="text-xl"/>
                </div>
                <div class="flex flex-col -space-y-1.5">
                    <span class="font-semibold text-gray-700">Acme</span>
                    <span class="text-xs text-gray-400">Supplies</span>
                </div>
            </div>

            <div class="flex items-center font-semibold">
                -$42.42
            </div>
        </div>
    </Card.Content>
</Card.Root>


<!--<Card.Root class="rounded-2xl">-->
<!--    <Card.Header>-->
<!--        <Card.Title class="flex justify-between">-->
<!--        </Card.Title>-->
<!--    </Card.Header>-->

<!--    <Card.Content>-->
<!--        <CreditCard cardNumber="**** **** **** 4242" balance={4242.42}/>-->
<!--    </Card.Content>-->

<!--    <Card.Footer class="flex justify-between font-semibold">-->
<!--                <span>-->
<!--                    Total available:-->
<!--                </span>-->
<!--        <Badge variant="outline">-->
<!--            $4242.42-->
<!--        </Badge>-->
<!--    </Card.Footer>-->
<!--</Card.Root>-->

<div class="my-10"></div>