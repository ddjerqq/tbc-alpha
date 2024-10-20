<script>
    export let cardNumber = "**** **** **** 4242";
    export let balance = 4242.42;

    let className = undefined;
    export { className as class };
</script>

<div class="{className} w-full aspect-[11/4] px-7 py-5 flex flex-col justify-center rounded-2xl bg-gradient-to-r from-sky-400 to-sky-500 text-white">
    <span class="font-mono mt-5 mb-10">
        {cardNumber}
    </span>

    <div class="flex justify-between items-end">
        <div class="flex flex-col">
            <span>Balance</span>
            <span class="text-2xl font-inter font-bold">${balance}</span>
        </div>

        <div class="flex">
            <div class="rounded-full bg-[#EB001B] size-8"></div>
            <div class="rounded-full bg-[#F79E1B] size-8 -translate-x-3"></div>
        </div>
    </div>
</div>