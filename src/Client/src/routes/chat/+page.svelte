<script>
  import {Badge} from "$lib/components/ui/badge/index.js";
  import CreditCard from "$lib/components/CreditCard.svelte";

  import * as Card from "$lib/components/ui/card/index.js";
</script>

<Card.Root class="rounded-2xl">
    <Card.Header>
        <Card.Title class="flex justify-between">
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