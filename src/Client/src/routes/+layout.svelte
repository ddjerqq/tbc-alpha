<script>
  import Icon from "heroicons-for-svelte";
  import {Home, SwitchHorizontal, User} from "heroicons-for-svelte/icons/outline";
</script>

<div class="w-full h-full flex flex-col justify-between">
    <slot></slot>

    <footer class="w-full bg-gray-300 flex justify-around items-center align-middle p-3">
        <a href="/" class="bg-gray-400 p-3 rounded-md">
            <Icon icon={Home} class="text-2xl"/>
        </a>

        <a href="/" class="bg-gray-400 p-3 rounded-md">
            <Icon icon={SwitchHorizontal} class="text-2xl"/>
        </a>

        <a href="/" class="bg-gray-400 p-3 rounded-md">
            <Icon icon={User} class="text-2xl"/>
        </a>
    </footer>
</div>